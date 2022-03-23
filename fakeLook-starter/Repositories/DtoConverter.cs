using fakeLook_models.Models;
using fakeLook_models.Models.dtoModels;
using fakeLook_starter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Services
{
    public class DtoConverter : IDtoConverter
    {
        public Comment DtoComment(Comment comment)
        {
            return new Comment() { Id = comment.Id, Content = comment.Content };
        }

        public Like DtoLike(Like like)
        {
            return new Like() { IsActive = like.IsActive };
        }

        public Post DtoPost(Post post)
        {
            return new Post() { Id = post.Id, Date = post.Date, Description = post.Description, ImageSorce = post.ImageSorce, X_Position = post.X_Position, Y_Position = post.Y_Position, Z_Position = post.Z_Position, };
        }

        public Tag DtoTag(Tag tag)
        {
            return new Tag() { Id = tag.Id, Content = tag.Content };
        }

        //public dtoUser DtoUser(User user)
        //{
        //    return new dtoUser() { Id = user.Id, UserName = user.UserName };
        //}

        public User DtoUser(User user)
        {
            return new User() { Id = user.Id, UserName = user.UserName };
        }

        public UserTaggedComment DtoUserTaggedComment(UserTaggedComment userTaggedComment)
        {
            return new UserTaggedComment() { UserId = userTaggedComment.UserId };
        }

        public UserTaggedPost DtoUserTaggedPost(UserTaggedPost userTaggedPost)
        {
            return new UserTaggedPost() { UserId = userTaggedPost.UserId };
        }
    }
}

        //------------------------------------------------
        // dto Section
        //------------------------------------------------

//        public dtoComment DtoComment(dtoComment comment)
//        {
//            return new dtoComment() { Id = comment.Id, Content = comment.Content };
//        }

//        public Like DtoLike(Like like)
//        {
//            return new Like() { IsActive = like.IsActive };
//        }

//        public dtoPost DtoPost(dtoPost post)
//        {
//            return new dtoPost() { Id = post.Id, Date = post.Date, Description = post.Description, ImageSorce = post.ImageSorce, X_Position = post.X_Position, Y_Position = post.Y_Position, Z_Position = post.Z_Position, };
//        }

//        public dtoTag DtoTag(dtoTag tag)
//        {
//            return new dtoTag() { Id = tag.Id, Content = tag.Content };
//        }

//        //public dtoUser DtoUser(User user)
//        //{
//        //    return new dtoUser() { Id = user.Id, UserName = user.UserName };
//        //}

//        public User DtoUser(User user)
//        {
//            return new User() { Id = user.Id, UserName = user.UserName };
//        }

//        public dtoUserTaggedComment DtoUserTaggedComment(dtoUserTaggedComment userTaggedComment)
//        {
//            return new dtoUserTaggedComment() { UserId = userTaggedComment.UserId };
//        }

//        public dtoUserTaggedPost DtoUserTaggedPost(dtoUserTaggedPost userTaggedPost)
//        {
//            return new dtoUserTaggedPost() { UserId = userTaggedPost.UserId };
//        }
//    }
//}
