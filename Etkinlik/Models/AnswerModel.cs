using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Etkinlik.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string AnswerName { get; set; }

        [Required]
        public int Vote { get; set; }

        public SurveyModel SurveyModel { get; set; }
    }
}
