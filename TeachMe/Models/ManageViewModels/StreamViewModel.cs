using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.ManageViewModels
{
    public class StreamViewModel
    {
        [Display(Name = "Stream link")]
        public string StreamLink { get; set; }

        public string Tittle { get; set; }
    }
}
