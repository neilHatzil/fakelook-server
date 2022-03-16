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

        public PostRepository(DataContext context, IDtoConverter dtoConverter)
        {
            _context = context;
            _dtoConverter = dtoConverter;
        }

        public async Task<Post> AddPost(Post item)
        {
            var res = _context.Posts.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public Post GetById(int id)
        {
            return _context.Posts.SingleOrDefault(p => p.Id == id);
        }

        public async Task<Post> EditPost(Post item)
        {
            var res = _context.Posts.Update(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Post> DeletePost(Post item)
        {
            var res = _context.Posts.Remove(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _context.Posts.OrderByDescending(d => d.Date).Include(p => p.User).Include(p => p.Comments).ThenInclude(c => c.User).Select(DtoLogic).ToList();
            return posts;
        }

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
            dtoPost.User = _dtoConverter.DtoUser(post.User);
            dtoPost.Comments = post.Comments.Select(c =>
            {
                var dtoComment = _dtoConverter.DtoComment(c);
                dtoComment.User = _dtoConverter.DtoUser(c.User);
                return dtoComment;
            }).ToArray();
            dtoPost.Likes = post.Likes.Select(c =>
            {
                var dtoLike = _dtoConverter.DtoLike(c);
                dtoLike.User = _dtoConverter.DtoUser(c.User);
                return dtoLike;
            }).ToArray();

            return dtoPost;
        }

    }
}
