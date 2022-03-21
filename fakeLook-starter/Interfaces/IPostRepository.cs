using fakeLook_models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface IPostRepository
    {
        public Task<Post> AddPost(Post item);//, ICollection<string> taggedUsers);
        public Post GetById(int id);
        public Task<Post> EditPost(Post item);
        public Task<Post> DeletePost(int itemId);
        public Task<IEnumerable<Post>> GetAllPosts();
        public Task<Post> LikeUnlike(int postId, int userId);
        public Task<Post> AddComment(Comment item);
        public ICollection<Post> GetByPredicate(Func<Post, bool> predicate);
        public string ConvertUserIdToUserName(int userId);
    }

}
