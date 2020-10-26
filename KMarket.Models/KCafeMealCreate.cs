using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Models
{
    public class KCafeMealCreate
    {

        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(1000, ErrorMessage = "There are too many characters in this field.")]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [MaxLength(8000)]
        [Required]
        public string Description { get; set; }

        [MaxLength(8000)]
        [Required]
        public string Ingredients { get; set; }


    }
}
