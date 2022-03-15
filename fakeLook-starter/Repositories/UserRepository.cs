using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using fakeLook_starter.Utilities;

namespace fakeLook_starter.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        // Get User data by id
        public async Task<User> Get(string userName)
        {
            // Change to userName + password after hash
            var res = _context.Users.SingleOrDefault(user => user.UserName == userName);
            //???? instead of saving into database?
            await _context.SaveChangesAsync();
            return res;
        }

        public User FindItem(User item)
        {
            // find by userName and hashed password
            item.Password = Utilities.Utilities.CreateHashCode(item.Password);
            return _context.Users.SingleOrDefault(p => p.UserName == item.UserName && p.Password == item.Password);
        }

        // Get All Users
        public ICollection<User> GetAll()
        {
            var users = _context.Users.ToList();
            return users;
        }

        // Add new User
        public async Task<User> Add(User item)
        {
            item.Password = Utilities.Utilities.CreateHashCode(item.Password);
            var res = _context.Users.Add(item);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public User Post(User item)
        {
            item.Password = Utilities.Utilities.CreateHashCode(item.Password);
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }

        // Update User
        public Task<User> Update(User item)
        {
            throw new NotImplementedException();
        }

        // Delete User
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
