using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Data
{
    //defines the meals hosted at KCafe
    public class KGrocerItem
    {

        [Key]
        public int ItemID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int DaysToExpire { get; set; }

        [Required]
        public DateTimeOffset AddedUTC { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
