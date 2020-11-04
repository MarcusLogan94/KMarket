using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ObjectID { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public string Name { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset AddedUTC { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
