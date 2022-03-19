using fakeLook_models.Models;
using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface ICommentRepository
    {
        public Task<Comment> AddComment(Comment item);
    }
}
