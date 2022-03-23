using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakeLook_models.Models.dtoModels
{
    public class dtoPost
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageSorce { get; set; }
        public double X_Position { get; set; }
        public double Y_Position { get; set; }
        public double Z_Position { get; set; }
        public DateTime Date { get; set; }

        /* EF Relations */
        public virtual ICollection<dtoLike> Likes { get; set; }
        public virtual dtoUser User { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<dtoComment> Comments { get; set; }
        public virtual ICollection<dtoTag> Tags { get; set; }
        public virtual ICollection<dtoUserTaggedPost> UserTaggedPost { get; set; }
    }
}
