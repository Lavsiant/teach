using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models
{
    public class PayViewModel
    {
        public decimal Price { get; set; }

        [Required]
        [MaxLength(16, ErrorMessage = "Card number must has 16 numbers")]
        [MinLength(16, ErrorMessage = "Card number must has 16 numbers")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Card number must be numeric")]
        public string CardNumber { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "CVV must has 3 numbers")]
        [MinLength(3, ErrorMessage = "CVV  must has 3 numbers")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CVV must be numeric")]
        [DataType(DataType.Password)]
        public string CVV { get; set; }

        [Required]
        [RegularExpression("^[0-1][0-9]*/[0-9][0-9]$", ErrorMessage = "Date must be in format xx/xx")]
        public string Date { get; set; }
    }
}
