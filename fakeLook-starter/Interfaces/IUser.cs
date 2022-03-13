using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface IUser<T>
    {
        public Task<T> Add(T item);
    }
}
