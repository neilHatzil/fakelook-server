using fakeLook_models.Models.dtoModels;

namespace fakeLook_models.Models.dtoModels
{
    public class dtoComment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        /* EF Relations */
        public int UserId { get; set; }
        public virtual dtoUser User { get; set; }
        //public int PostId { get; set; }

    }
}
