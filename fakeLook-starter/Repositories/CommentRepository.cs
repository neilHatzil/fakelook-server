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
        readonly private IUserRepository _userRepository;

        public CommentRepository(DataContext context, IDtoConverter dtoConverter, ITagRepository tagRepository, IUserRepository userRepository)
        {
            _context = context;
            _dtoConverter = dtoConverter;
            _tagRepository = tagRepository;
            _userRepository = userRepository;
        }

        public async Task<Comment> AddComment(Comment item)
        {
            List<Tag> tags = new List<Tag>();
            List<Tag> tagsList = new List<Tag>();
            List<UserTaggedComment> userTaggedList = new List<UserTaggedComment>();
            string userName = item.User.UserName;
            tagsList = item.Tags.ToList();
            userTaggedList = item.UserTaggedComment.ToList();
            item.User = null;
            // Clear the post's tags and userTagged
            item.Tags.Clear();
            item.UserTaggedComment.Clear();
            // Add tags to comment -tag table
            tags = await AddTagsOnComment(tagsList);
            // Add Comment to context
            var res = _context.Comments.Add(item);
            foreach (var tag in tags)
            {
                res.Entity.Tags.Add(tag);
            }
            // Add new userTagged to comment
            foreach (var userTagged in userTaggedList)
            {
                int id = _userRepository.GetByUserName(userTagged.User.UserName).Id;
                res.Entity.UserTaggedComment.Add(new UserTaggedComment { UserId = id });
            }
            //res.Entity.User.UserName = userName;
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        private async Task<List<Tag>> AddTagsOnComment(ICollection<Tag> tags)
        {
            // Add Tags to context
            return await _tagRepository.AddTags(tags.ToList());
        }

        //private Comment GetById(int id)
        //{
        //    var comment = _context.Comments
        //        .Include(c => c.Tags)
        //        .Include(c => c.UserTaggedComment)
        //        .Select(DtoLogic)
        //        .SingleOrDefault(c => c.Id == id);
        //    return comment;
        //}

        //private Comment DtoLogic(Comment comment)
        //{
        //    var dtoComment = _dtoConverter.DtoComment(comment);
        //    // Content
        //    dtoComment.Content = comment.Content;
        //    // User ID
        //    dtoComment.UserId = comment.UserId;
        //    // Post Id
        //    dtoComment.PostId = comment.PostId;
        //    // Tags
        //    dtoComment.Tags = comment.Tags?.Select(t =>
        //    {
        //        var dtoTag = _dtoConverter.DtoTag(t);
        //        return dtoTag;
        //    }).ToArray();
        //    // UserTaggedComment
        //    dtoComment.UserTaggedComment = comment.UserTaggedComment?.Select(u =>
        //    {
        //        var dtoTaggedComment = _dtoConverter.DtoUserTaggedComment(u);
        //        return dtoTaggedComment;
        //    }).ToArray();

        //    return dtoComment;
        //}
    }
}
