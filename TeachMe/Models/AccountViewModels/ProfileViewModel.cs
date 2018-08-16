using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.AccountViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }

        public List<string> Subjects { get; set; }

        public double Rating { get; set; }

        public bool isReadyForRate { get; set; }

        public bool isReadyForComment { get; set; }

        public List<string> CourseTittles { get; set; }

        public string CommentText { get; set; }

        public bool IsReadyForCreate { get; set; }

        public bool isShowCertificates { get; set; }




    }
}
