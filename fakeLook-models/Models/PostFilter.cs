using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakeLook_models.Models
{
    public class PostFilter
    {
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public ICollection<string> Publishers { get; set; }
        public ICollection<string> Tags { get; set; }
        public ICollection<string> TaggedUsers { get; set; }

        public bool checkDate(DateTime postDate)//, DateTime filterStartingDate, DateTime filterEndingDate)
        {
            if (StartingDate.Equals(DateTime.MinValue) || EndingDate.Equals(DateTime.MinValue)) return true;
            if (postDate >= StartingDate && postDate <= EndingDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkPublishers(string UserName)//, ICollection<string> Publishers)
        {
            if(Publishers == null || Publishers.Count == 0) return true;
            if (Publishers.Contains(UserName)) return true;
            return false;
        }
        public bool checkTaggs(ICollection<Tag> postTags)//, ICollection<string> filterTags)
        {
            if (Tags == null || Tags.Count == 0)
            {
                return true;
            }
            if (postTags == null || postTags.Count == 0) { return false; }

            foreach (var tag in Tags)
            {
                foreach (var postTag in postTags)
                {
                    if (tag.Equals(postTag.Content))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool checkTaggedUsers(ICollection<UserTaggedPost> postTaggedUsers)//, ICollection<string> filterTaggedUsers)
        {
            // TaggedUsers from the user - check if it's empty
            if (TaggedUsers == null || TaggedUsers.Count == 0)
            {
                return true;
            }
            // TaggedUsers from the post - check if it's empty
            if (postTaggedUsers == null || postTaggedUsers.Count == 0) { return false; }
            // Both aren't empty
            foreach (var taggedUser in TaggedUsers)
            {
                foreach (var postTaggeuser in postTaggedUsers)
                {
                    if (taggedUser.Equals(postTaggeuser.User.UserName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}