﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Etkinlik
{
    public class UserModel : IdentityUser
    {
        /*public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(25)]
        public string FullName { get; set; }*/

    }
}
