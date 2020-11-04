using KMarket.Data;
using KMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Services
{
    public class KCafeMealService
    {
        private readonly Guid _userId;

        public KCafeMealService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateKCafeMeal(KCafeMealCreate model)
        {
            var entity =
                new KCafeMeal()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Ingredients = model.Ingredients,
                    AddedUTC = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now

                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.KCafeMeals.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<KCafeMealListItem> GetKCafeMeals()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .KCafeMeals
                        .Where(e => e.MealID != null)
                        .Select(
                            e =>
                                new KCafeMealListItem
                                {
                                    MealID = e.MealID,
                                    Name = e.Name,
                                    Price = e.Price,
                                    Description = e.Description,
                                    Ingredients = e.Ingredients,
                                    AddedUTC = e.AddedUTC
                                }
                        );

                return query.ToArray();
            }
        }

        public KCafeMealDetail GetKCafeMealByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .KCafeMeals
                        .Single(e => e.MealID == id);
                return
                    new KCafeMealDetail
                    {
                        MealID = entity.MealID,
                        Name = entity.Name,
                        Price = entity.Price,
                        Description = entity.Description,
                        Ingredients = entity.Ingredients,
                        AddedUTC = entity.AddedUTC
                    };
            }
        }

        public bool UpdateKCafeMeal(KCafeMealEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .KCafeMeals
                        .Single(e => e.MealID == model.MealID);

                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Description = model.Description;
                entity.Ingredients = model.Ingredients;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteKCafeMeal(int mealID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .KCafeMeals
                        .Single(e => e.MealID == mealID);

                ctx.KCafeMeals.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
