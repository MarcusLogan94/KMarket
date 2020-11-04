using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Data
{
    // defines the order class for tracking orders of a specific type of item
    // (one item type at a time right now, not multiple items in order)
    public class OrderItem
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public Guid OwnerID { get; set; }
        public Guid LastModifiedID { get; set; }

        [ForeignKey("Item")]
        [Required]
        public int ItemID { get; set; }

        [Required]
        public KGrocerItem Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public DateTimeOffset AddedUTC { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}

