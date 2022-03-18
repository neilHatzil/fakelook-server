﻿using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class PostRepository : IPostRepository
    {
        readonly private DataContext _context;
        private readonly IDtoConverter _dtoConverter;
        readonly private ITagRepository _tagRepository;

        public PostRepository(DataContext context, IDtoConverter dtoConverter, ITagRepository tagRepository)
        {
            _context = context;
            _dtoConverter = dtoConverter;
            _tagRepository = tagRepository;
        }

        public async Task<Post> AddPost(Post item)
        {
            // Add tags to post
            AddTagsOnPost(item.Tags);
            // Add tag to context
            var res = _context.Posts.Add(item);
            // Add userTagged to post to post
            res.Entity.UserTaggedPost.Union(item.UserTaggedPost);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public Post GetById(int id)
        {
            var posts = _context.Posts
                .Include(p => p.Likes)
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Tags)
                .Include(p => p.Comments)
                .ThenInclude(c => c.UserTaggedComment)
                .Select(DtoLogic)
                .SingleOrDefault(p => p.Id == id);
            return posts; 
        }

        public async Task<Post> EditPost(Post item)
        {
            var res = _context.Posts.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        // send only id and call inside function get by id
        public async Task<Post> DeletePost(int id)
        {
            var post = GetById(id);
            var res = _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _context.Posts
                .OrderByDescending(d => d.Date)
                .Include(p => p.Likes)
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Tags)
                .Include(p => p.Comments)
                .ThenInclude(c => c.UserTaggedComment)
                .Select(DtoLogic).ToList();
            return posts;
        }

        // Add Like to post if not exist or change IsActive if exists
        public async Task<Post> LikeUnlike(int postId, int userId)
        {
            //Post post = GetById(postId);
            ////_context.Posts.SingleOrDefault(p => p.id == postId);
            //Like like = post.Likes
            //    .Where(l => l.PostId == postId && l.UserId == userId)
            //    .SingleOrDefault();

            //if(like == null)
            //{
            //    // Like doesn't exists - add a new one
            //   _context.Posts.Where(p => p.Id == postId).SingleOrDefault().Likes
            //        .Append(new Like { IsActive = true ,UserId = userId, PostId = postId});
            //}
            //else
            //{
            //    // boolean XOR on IsActive
            //    bool l = like.IsActive^true;
            //    // Like exists - change IsActive of the like
            //   _context.Posts.Where(p => p.Id == postId).SingleOrDefault()
            //            .Likes.Where(l => l.UserId == userId).SingleOrDefault().IsActive = l;
            //}

            //post = GetById(postId);
            //var res;
            var like = _context.Likes.Where(l => l.PostId == postId && l.UserId == userId).SingleOrDefault();
            if (like == null)
            {
                // Like doesn't exists - add a new one
                var res = _context.Likes
                     .Add(new Like { IsActive = true, UserId = userId, PostId = postId });
            }
            else
            {
                // boolean XOR on IsActive
                bool l = like.IsActive ^ true;
                like.IsActive = l;
                // Like exists - change IsActive of the like
               var res = _context.Likes.Update(like);
            }

            Post post = GetById(postId);
            await _context.SaveChangesAsync();
            return post;
        }

        private void AddTagsOnPost(ICollection<Tag> tags)
        {
            // Add Tags to context and receive a list of tags
            _tagRepository.AddTags(tags);
        }

        //private void AddTaggedUsersOnPost(ICollection<UserTaggedPost> userTaggedPost, Post post )
        //{
        //    post.UserTaggedPost.Union(userTaggedPost);
        //}

        //public Post FindItem(Post item)
        //{
        //    throw new NotImplementedException();
        //}

        //public ICollection<Post> GetAll()
        //{
        //    return _context.Posts.ToList();
        //}


        //public ICollection<Post> GetByPredicate(Func<Post,bool> predicate)
        //{
        //    return _context.Posts.Where(predicate).ToList();
        //}

        private Post DtoLogic(Post post)
        {
            var dtoPost = _dtoConverter.DtoPost(post);
            // User
            //dtoPost.User = _dtoConverter.DtoUser(post.User);
            // User ID
            dtoPost.UserId = post.UserId;
            // Comments
            dtoPost.Comments = post.Comments?.Select(c =>
            {
                var dtoComment = _dtoConverter.DtoComment(c);
                // User of the comment
                //dtoComment.User = _dtoConverter.DtoUser(c.User);
                // User ID of the comment
                dtoComment.UserId = c.UserId;
                // Tags of the comment
                dtoComment.Tags = c.Tags?.Select(t =>
                {
                    var dtoCommentTag = _dtoConverter.DtoTag(t);
                    return dtoCommentTag;
                }).ToArray();
                // UserTags of the comment
                dtoComment.UserTaggedComment = c.UserTaggedComment?.Select(t =>
                {
                    var dtoUserTaggedComment = _dtoConverter.DtoUserTaggedComment(t);
                    return dtoUserTaggedComment;
                }).ToArray();
                return dtoComment;
            }).ToArray();
            // Likes
            dtoPost.Likes = post.Likes?.Select(c =>
            {
                var dtoLike = _dtoConverter.DtoLike(c);
                // Like Id of like
                dtoLike.Id = c.Id;
                // User of the like
                //dtoLike.User = _dtoConverter.DtoUser(c.User);
                // IsActive of the like
                dtoLike.IsActive = c.IsActive;
                // UserId of like
                dtoLike.UserId = c.UserId;
                // PostId of like
                dtoLike.PostId = c.PostId;
                return dtoLike;
            }).ToArray();
            // Tags
            dtoPost.Tags = post.Tags?.Select(c =>
            {
                var dtoTag = _dtoConverter.DtoTag(c);
                return dtoTag;
            }).ToArray();
            // UserTaggedPost
            dtoPost.UserTaggedPost = post.UserTaggedPost?.Select(u =>
            {
                var dtoTaggedPost = _dtoConverter.DtoUserTaggedPost(u);
                return dtoTaggedPost;
            }).ToArray();

            return dtoPost;
        }
    }
}
