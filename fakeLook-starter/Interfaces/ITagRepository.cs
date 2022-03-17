using fakeLook_models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface ITagRepository
    {
        public Task<List<Tag>> AddTags(ICollection<Tag> items);

        public Tag GetById(int id);
    }
}
