using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Models
{
    public class OrderCreate
    {

        [Required]
        public int ObjectID { get; set; }

        [Required]
        public string OrderType { get; set; }

        [Required]
        public int Quantity { get; set; }


    }
}
