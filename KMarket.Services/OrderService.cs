using KMarket.Data;
using KMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace KMarket.Services
{
    public class OrderService
    {
        private readonly Guid _userId;

        public OrderService(Guid userId)
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
                else if (DBName == "Item")
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
        public bool CreateOrder(OrderCreate model)
        {

            using (var ctx = new ApplicationDbContext())
            {
                //declare pricecalculation, and then assigns correct Calculated price based on the correct DB object's Price
                double priceCalculation = 0;
                if (model.OrderType == "Meal")
                {
                    var ReferredMeal =
                         ctx
                           .KCafeMeals
                           .Single(e => e.MealID == model.ObjectID);

                    priceCalculation = (double)model.Quantity * ReferredMeal.Price;

                }
                else if (model.OrderType == "Item")
                {
                    var ReferredItem =
                        ctx
                          .KGrocerItems
                          .Single(e => e.ItemID == model.ObjectID);

                    priceCalculation = (double)model.Quantity * ReferredItem.Price;
                }

                var entity =
                new Order()
                {
                    OwnerID = _userId,
                    LastModifiedID = _userId,
                    ObjectID = model.ObjectID,
                    OrderType = model.OrderType,
                    Quantity = model.Quantity,
                    TotalPrice = priceCalculation,
                    AddedUTC = DateTimeOffset.UtcNow,
                    ModifiedUtc = DateTimeOffset.UtcNow,
            };

                ctx.Orders.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<OrderListItem> GetOrders()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Orders
                    .Where(e => e.OrderID != null)
                    .Select(
                            e =>
                                new OrderListItem
                                {
                                    OrderID = e.OrderID,
                                    ObjectID = e.ObjectID,
                                    OrderType = e.OrderType,
                                    Quantity = e.Quantity,
                                    TotalPrice = e.TotalPrice,
                                    AddedUTC = e.AddedUTC,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                IEnumerable<OrderListItem> queryArray = query.ToArray();

                for(int i = 0; i < queryArray.Count(); i++)
                {
                    queryArray.ElementAt(i).Name = GetNameOfObject(queryArray.ElementAt(i).OrderType, queryArray.ElementAt(i).ObjectID);
                }

                return queryArray;
            }
        }

        public OrderDetail GetOrderByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderID == id);
                return

                    new OrderDetail
                    {
                        OrderID = entity.OrderID,
                        ObjectID = entity.ObjectID,
                        OrderType = entity.OrderType,
                        Quantity = entity.Quantity,
                        TotalPrice = entity.TotalPrice,
                        Name = GetNameOfObject(entity.OrderType, entity.ObjectID),
                        AddedUTC = entity.AddedUTC,
                        ModifiedUtc = entity.ModifiedUtc


                    };
            }
        }

        public bool UpdateOrder(OrderEdit model)
        {

            using (var ctx = new ApplicationDbContext())
            {

                double calculatedPrice = 0;

                if(model.OrderType == "Meal")
                {
                    calculatedPrice = ctx.KCafeMeals.Single(e => e.MealID == model.ObjectID).Price * (double) model.Quantity;
                }
                else if (model.OrderType == "Item")
                {
                    calculatedPrice = ctx.KGrocerItems.Single(e => e.ItemID == model.ObjectID).Price * (double) model.Quantity;
                }

                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderID == model.OrderID);

                entity.ObjectID = model.ObjectID;
                entity.OrderType = model.OrderType;
                entity.Quantity = model.Quantity;
                entity.TotalPrice = calculatedPrice;
                entity.LastModifiedID = _userId;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteOrder(int orderID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Orders
                        .Single(e => e.OrderID == orderID);

                ctx.Orders.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
