using fakeLook_models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface IRepository<T>
    {
        public T Post(T item);
        public T GetById(string id);
        public T FindItem(T item);
        //public ICollection<T> GetByPredicate(Func<T, bool> predicate);
    }
    //public interface IUser : IRepository<User>
    //{

    //}
    public interface IPostRepository : IRepository<Post>
    {

    }
}
