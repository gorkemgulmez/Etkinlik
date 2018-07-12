using Etkinlik.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Etkinlik
{
    public class UserPostModel 
    {
        public int Id { get; set; }

        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser{ get; set; }

        [ForeignKey("PostModelId")]
        public virtual PostModel PostModel { get; set; }
        public int? PostModelId { get; set; }
    }
}
