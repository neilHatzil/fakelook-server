using System.Collections.Generic;
using System.Threading.Tasks;
using fakeLook_models.Models;

namespace fakeLook_starter.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Add(User item);

        public ICollection<User> GetAll();

        public Task<User> Get(int id);
        public Task<User> Update(User item);
        public Task Delete(int id);
    }
}
