using Etkinlik.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Etkinlik
{
    [Table("UserPosts")]
    public class UserPostModel 
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public PostModel PostModel { get; set; }
    }
}
