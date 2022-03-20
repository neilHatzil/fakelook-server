using fakeLook_dal.Data;
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
        readonly private ICommentRepository _commentRepository;
        readonly private IUserRepository _userRepository;

        public PostRepository(DataContext context, IDtoConverter dtoConverter, ITagRepository tagRepository, ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _context = context;
            _dtoConverter = dtoConverter;
            _tagRepository = tagRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task<Post> AddPost(Post item)//,ICollection<string> taggedUsers)
        {
            List<Tag> tags = new List<Tag>();
            List<Tag> tagsList = new List<Tag>();
            List<UserTaggedPost> taggedUserList = new List<UserTaggedPost>();
            tagsList = item.Tags.ToList();
            taggedUserList = item.UserTaggedPost.ToList();
            // Clear the Taggs of the post
            item.Tags.Clear();
            item.UserTaggedPost.Clear();
            // Add tags to post - tag table
            tags = await AddTagsOnPost(tagsList);
            // Add tag to context
            var res = _context.Posts.Add(item);
            // Add Taggs to post
            foreach (var tag in tags)
            {
                res.Entity.Tags.Add(tag);
            }
            // Add userTagged to post to post
            foreach (var userTagged in taggedUserList)
            {
                int id = _userRepository.GetByUserName(userTagged.User.UserName).Id;
                res.Entity.UserTaggedPost.Add(new UserTaggedPost { UserId = id , PostId  = item.Id});
            }
            //res.Entity.UserTaggedPost.Union(item.UserTaggedPost);
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
            List<Tag> tags = new List<Tag>();
            List<Tag> tagsList = new List<Tag>();
            List<UserTaggedPost> userTaggedList = new List<UserTaggedPost>();
            tagsList = item.Tags.ToList();
            userTaggedList = item.UserTaggedPost.ToList();
            // Clear the post's tags and userTagged
            item.Tags.Clear();
            item.UserTaggedPost.Clear();
            // Clear the post's tags and userTagged from context
            var tagsC = _context.Posts
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .Where(p => p.Id == item.Id).SingleOrDefault();
            tagsC.Tags.Clear();
            tagsC.UserTaggedPost.Clear();
            // Add tags to post - tag table
            tags = await AddTagsOnPost(tagsList);
            // Update the post without the tags 
            var res = _context.Posts.Update(tagsC);
            // Add new Taggs to post
            foreach (var tag in tags)
            {
                res.Entity.Tags.Add(tag);
            }
            // Add new userTagged to post
            foreach (var userTagged in userTaggedList)
            {
                int id = _userRepository.GetByUserName(userTagged.User.UserName).Id;
                res.Entity.UserTaggedPost.Add(new UserTaggedPost { UserId = id, PostId = item.Id });
                //res.Entity.UserTaggedPost.Add(userTagged);
            }
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

        public async Task<Post> AddComment(Comment item)
        {
            // Validation on userId
            // Validation on postId

            // Add Comment through the CommentRepository
            var comment = await _commentRepository.AddComment(item);
            // Add Comment to post
            Post post = GetById(item.PostId);
            //await _context.SaveChangesAsync();
            return post;
        }

        public ICollection<Post> GetByPredicate(Func<Post, bool> predicate)
        {
            return _context.Posts
                .Include(p => p.Tags)
                .Include(p => p.UserTaggedPost)
                .ThenInclude(p => p.User)
                .Select(DtoLogicReduced)
                .Where(predicate).ToList();
            
        }

        public string ConvertUserIdToUserName(int userId)
        {
            string userName = string.Empty;
            userName = _context.Users.Where(u => u.Id == userId)
                .SingleOrDefault().UserName;
            return userName;
        }
        private async Task<List<Tag>> AddTagsOnPost(List<Tag> tags)
        {
            // Add Tags to context
            return await _tagRepository.AddTags(tags);
        }


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

        private Post DtoLogicReduced(Post post)
        {
            var dtoPost = _dtoConverter.DtoPost(post);
            // User ID
            dtoPost.UserId = post.UserId;
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
                dtoTaggedPost.User = _dtoConverter.DtoUser(u.User);
                return dtoTaggedPost;
            }).ToArray();

            return dtoPost;
        }


    }
}
