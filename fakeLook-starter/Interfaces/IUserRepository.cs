using System.Collections.Generic;
using System.Threading.Tasks;
using fakeLook_models.Models;

namespace fakeLook_starter.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Get(string userName);

        // For Authentication
        public User FindItem(User item);
        public User GetById(string id);
        public ICollection<User> GetAll();
        public Task<User> Add(User item);
        
        // For Authentication
        public User Post(User item);
        public Task<User> Update(User item);
        public Task Delete(int id);

    }
}
