using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Etkinlik.Models
{
    public class SurveyModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title {get; set;}
    }
}
