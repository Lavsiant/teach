using Model.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.CourseModel
{
    public class CourseMark : IRating
    {
        [Key]
        public int ID { get; set; }

        [Range(0, 5)]
        public double Mark { get; set; }

        public string RaterId { get; set; }
    }
}
