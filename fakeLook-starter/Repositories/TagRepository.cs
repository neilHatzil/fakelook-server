using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class TagRepository:ITagRepository
    {
        readonly private DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> AddTags(ICollection<Tag> values)
        {
            List<Tag> tagsList = new List<Tag>();
            foreach (var item in values)
            {
                var tag = _context.Tags.SingleOrDefault(p => p.Content == item.Content);
                if (tag != null)
                {
                    // Get id - add to id's array
                    tagsList.Add(tag);
                }
                else
                {
                    // Add Tag to Db
                    var res = _context.Tags.Add(item);
                    tagsList.Add(tag);
                }
            }
            await _context.SaveChangesAsync();
            return tagsList;
        }

        public Tag GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
