using fakeLook_dal.Data;
using fakeLook_models.Models;
using fakeLook_starter.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        readonly private DataContext _context;
        private readonly IDtoConverter _dtoConverter;
        readonly private ITagRepository _tagRepository;

        public CommentRepository(DataContext context, IDtoConverter dtoConverter, ITagRepository tagRepository)
        {
            _context = context;
            _dtoConverter = dtoConverter;
            _tagRepository = tagRepository;
        }

        public async Task<Comment> AddComment(Comment item)
        {
            // Add tags to comment -tag table
            AddTagsOnComment(item.Tags);
            // Add Comment to context
            var res = _context.Comments.Add(item);
            // Add TaggedUsers to comment to comment
            res.Entity.UserTaggedComment.Union(item.UserTaggedComment);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        private void AddTagsOnComment(ICollection<Tag> tags)
        {
            // Add Tags to context
            _tagRepository.AddTags(tags.ToList());
        }

        private Comment GetById(int id)
        {
            var comment = _context.Comments
                .Include(c => c.Tags)
                .Include(c => c.UserTaggedComment)
                .Select(DtoLogic)
                .SingleOrDefault(c => c.Id == id);
            return comment;
        }

        private Comment DtoLogic(Comment comment)
        {
            var dtoComment = _dtoConverter.DtoComment(comment);
            // Content
            dtoComment.Content = comment.Content;
            // User ID
            dtoComment.UserId = comment.UserId;
            // Post Id
            dtoComment.PostId = comment.PostId;
            // Tags
            dtoComment.Tags = comment.Tags?.Select(t =>
            {
                var dtoTag = _dtoConverter.DtoTag(t);
                return dtoTag;
            }).ToArray();
            // UserTaggedComment
            dtoComment.UserTaggedComment = comment.UserTaggedComment?.Select(u =>
            {
                var dtoTaggedComment = _dtoConverter.DtoUserTaggedComment(u);
                return dtoTaggedComment;
            }).ToArray();

            return dtoComment;
        }
    }
}
