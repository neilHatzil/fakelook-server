using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace fakeLook_starter.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly private DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        // Add new User
        public async Task<User> Add(User item)
        {
            var res = _context.Users.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        // Delete User
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        // Edit User
        public Task<User> Edit(User item)
        {
            throw new NotImplementedException();
        }

        // Get User data by id
        public Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        // Get All Users
        public ICollection<User> GetAll()
        {
            var users = _context.Users.ToList();
            return users;
        }

        // Update User
        public Task<User> Update(User item)
        {
            throw new NotImplementedException();
        }

        //public ICollection<User> GetByPredicate(Func<User, bool> predicate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
