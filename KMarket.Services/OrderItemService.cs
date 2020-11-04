using KMarket.Data;
using KMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Services
{
    public class OrderItemService
    {
        private readonly Guid _userId;

        public OrderItemService(Guid userId)
        {
            _userId = userId;
        }

        private string GetNameOfObject(string DBName, int objectID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                string name = "";

                if (DBName == "Meal")
                {
                    var ReferredMeal =
                        ctx
                        .KCafeMeals
                        .Single(e => e.MealID == objectID);

                    name = ReferredMeal.Name;
                }
                else if (true)
                {
                    var ReferredItem =
                        ctx
                        .KGrocerItems
                        .Single(e => e.ItemID == objectID);

                    name = ReferredItem.Name;
                }
                return name;
            }
        }
        public bool CreateOrderItem(OrderItemCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var ReferredItem =
                          ctx
                              .KGrocerItems
                              .Single(e => e.ItemID == model.ItemID);

                double priceCalculation = (double)model.Quantity * ReferredItem.Price;



                var entity =
                    new OrderItem()
                    {
                        OwnerID = _userId,
                        LastModifiedID = _userId,
                        ItemID = model.ItemID,
                        Item = ReferredItem,
                        Quantity = model.Quantity,
                        TotalPrice = priceCalculation,
                        AddedUTC = DateTimeOffset.UtcNow,
                        ModifiedUtc = DateTimeOffset.UtcNow

                    };

                ctx.OrderItems.Add(entity);
                return ctx.SaveChanges() == 1;

            }

        }

        public IEnumerable<OrderItemListItem> GetOrderItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .OrderItems
                        .Where(e => e.ItemID != null)
                        .Select(
                            e =>
                                new OrderItemListItem
                                {
                                    OrderID = e.OrderID,
                                    ItemID = e.ItemID,
                                    Quantity = e.Quantity,
                                    TotalPrice = e.TotalPrice,
                                    Name = e.Item.Name,
                                    AddedUTC = e.AddedUTC,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public OrderItemDetail GetOrderItemByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .OrderItems
                        .Single(e => e.ItemID == id);
                return
                    new OrderItemDetail
                    {
                        OrderID = entity.OrderID,
                        ItemID = entity.ItemID,
                        Quantity = entity.Quantity,
                        TotalPrice = entity.TotalPrice,
                        Name = entity.Item.Name,
                        AddedUTC = entity.AddedUTC,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateItemOrder(OrderItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity =
                    ctx
                        .OrderItems
                        .Single(e => e.OrderID == model.OrderID);

                entity.ItemID = model.ItemID;
                entity.Item = ctx.KGrocerItems.Single(e => e.ItemID == model.ItemID);
                entity.Quantity = model.Quantity;
                entity.TotalPrice = entity.Item.Price * (double)model.Quantity;
                entity.LastModifiedID = _userId;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteOrderItem(int orderID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .OrderItems
                        .Single(e => e.OrderID == orderID);

                ctx.OrderItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
