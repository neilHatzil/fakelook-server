using fakeLook_models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface IPostRepository
    {
        public Task<Post> AddPost(Post item);
        public Post GetById(int id);
        public Task<Post> EditPost(Post item);
        public Task<Post> DeletePost(Post item);
        public IEnumerable<Post> GetAllPosts();
        public Task<Post> TagPost(Post item, Tag tag);

        //public ICollection<T> GetByPredicate(Func<T, bool> predicate);
    }

}
