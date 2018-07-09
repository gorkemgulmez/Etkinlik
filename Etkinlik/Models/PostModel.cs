using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etkinlik
{
    public class PostModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string PostName { get; set; }

        [MaxLength(1000)]
        [Required]
        public string PostDesc { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PostCreateTime { get; set; }

        public int UserModelId { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}
