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
    public class OrderMealService
    {
        private readonly Guid _userId;

        public OrderMealService(Guid userId)
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
        public bool CreateOrderMeal(OrderMealCreate model)
        {

            using (var ctx = new ApplicationDbContext())
            {
                //declare pricecalculation, and then assigns correct Calculated price based on the correct DB object's Price
                
                var ReferredMeal =
                         ctx
                           .KCafeMeals
                           .Single(e => e.MealID == model.MealID);

                double priceCalculation = (double)model.Quantity * ReferredMeal.Price;


                var entity =
                new OrderMeal()
                {
                    OwnerID = _userId,
                    LastModifiedID = _userId,
                    MealID = model.MealID,
                    Meal = ReferredMeal,
                    Quantity = model.Quantity,
                    TotalPrice = priceCalculation,
                    AddedUTC = DateTimeOffset.UtcNow,
                    ModifiedUtc = DateTimeOffset.UtcNow,
            };

                ctx.OrderMeals.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<OrderMealListItem> GetOrderMeals()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.OrderMeals
                    .Where(e => e.MealID != null)
                    .Select(
                            e =>
                                new OrderMealListItem
                                {
                                    OrderID = e.OrderID,
                                    MealID = e.MealID,
                                    Quantity = e.Quantity,
                                    TotalPrice = e.TotalPrice,
                                    Name = e.Meal.Name,
                                    AddedUTC = e.AddedUTC,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public OrderMealDetail GetOrderMealByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .OrderMeals
                        .Single(e => e.OrderID == id);
                return

                    new OrderMealDetail
                    {
                        OrderID = entity.OrderID,
                        MealID = entity.MealID,
                        Quantity = entity.Quantity,
                        TotalPrice = entity.TotalPrice,
                        Name = entity.Meal.Name,
                        AddedUTC = entity.AddedUTC,
                        ModifiedUtc = entity.ModifiedUtc

                    };
            }
        }

        public bool UpdateOrderMeal(OrderMealEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity =
                    ctx
                        .OrderMeals
                        .Single(e => e.OrderID == model.OrderID);

                entity.MealID = model.MealID;
                entity.Meal = ctx.KCafeMeals.Single(e => e.MealID == model.MealID);
                entity.Quantity = model.Quantity;
                entity.TotalPrice = entity.Meal.Price * (double) model.Quantity;
                entity.LastModifiedID = _userId;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteOrderMeal(int orderID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .OrderMeals
                        .Single(e => e.OrderID == orderID);

                ctx.OrderMeals.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
