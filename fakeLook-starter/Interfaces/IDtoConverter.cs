using fakeLook_models.Models;
//using fakeLook_models.Models.dtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakeLook_starter.Interfaces
{
    public interface IDtoConverter
    {
        public Post DtoPost(Post post);
        public User DtoUser(User user);
        public Comment DtoComment(Comment comment);
        public Tag DtoTag(Tag tag);
        public Like DtoLike(Like like);
        public UserTaggedComment DtoUserTaggedComment(UserTaggedComment userTaggedComment);
        public UserTaggedPost DtoUserTaggedPost(UserTaggedPost userTaggedPost);

        //---------------------------------------
        // dto Section
        //---------------------------------------
        //public dtoPost DtoPost(dtoPost post);
        ////public dtoUser DtoUser(User user);
        //public User DtoUser(User user);
        //public dtoComment DtoComment(dtoComment comment);
        //public dtoTag DtoTag(dtoTag tag);
        //public Like DtoLike(Like like);
        //public dtoUserTaggedComment DtoUserTaggedComment(dtoUserTaggedComment userTaggedComment);
        //public dtoUserTaggedPost DtoUserTaggedPost(dtoUserTaggedPost userTaggedPost);
    }
}
