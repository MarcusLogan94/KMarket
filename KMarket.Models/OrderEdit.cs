using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Models
{
    public class OrderEdit
    {
        public int OrderID { get; set; }
        public int ObjectID { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }

    }
}