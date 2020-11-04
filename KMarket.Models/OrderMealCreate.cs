using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Models
{
    public class OrderMealCreate
    {

        [Required]
        public int MealID { get; set; }

        [Required]
        public int Quantity { get; set; }


    }
}
