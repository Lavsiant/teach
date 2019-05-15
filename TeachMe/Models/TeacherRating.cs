using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models
{
    public class TeacherRating : IRating
    {
        [Key]
        public int ID { get; set; }

        [Range(0, 5)]
        public double Mark { get; set; }

        public string RaterId { get; set; }
    }
}
