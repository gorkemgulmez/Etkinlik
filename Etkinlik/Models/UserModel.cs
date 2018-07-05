using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Etkinlik
{
    public class UserModel
    {
        [Key]
        public int userID { get; set; }

        [Required]
        [MaxLength(25)]
        public string userName { get; set; }

        [Required]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required]
        [MaxLength(150)]
        public string userEmail { get; set; }

        [Required]
        [MaxLength(25)]
        public string fullName { get; set; }

    }
}
