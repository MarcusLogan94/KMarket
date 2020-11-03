using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Models
{
    public class OrderListItem
    {
        public int OrderID { get; set; }

        [Display(Name = "ObjectID")]
        public int ObjectID { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }


        public string Name { get; set; }



        [Display(Name = "Added")]
        public DateTimeOffset AddedUTC { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }


    }
}
