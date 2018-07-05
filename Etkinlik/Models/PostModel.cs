using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etkinlik
{
    public class PostModel
    {
        [Key]
        public int postID { get; set; }

        [MaxLength(100)]
        [Required]
        public string postName { get; set; }

        [MaxLength(1000)]
        [Required]
        public string postDesc { get; set; }

        public int refUserID { get; set; }

        [ForeignKey("refUserID")]
        public virtual UserModel UserModel { get; set; }
    }
}
