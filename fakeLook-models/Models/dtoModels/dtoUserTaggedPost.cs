using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakeLook_models.Models.dtoModels
{
    public class dtoUserTaggedPost
    {
        public int Id { get; set; }
        public virtual dtoUser User { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
