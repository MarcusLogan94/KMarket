using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Data
{
    //defines the meals hosted at KCafe
    public class KCafeMeal
    {

        [Key]
        public int MealID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public DateTimeOffset AddedUTC { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }



    }
}
