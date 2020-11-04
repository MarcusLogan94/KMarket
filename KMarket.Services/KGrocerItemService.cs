using KMarket.Data;
using KMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMarket.Services
{
    public class KGrocerItemService
    {
        private readonly Guid _userId;

        public KGrocerItemService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateKGrocerItem(KGrocerItemCreate model)
        {
            var entity =
                new KGrocerItem()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Category = model.Category,
                    DaysToExpire = model.DaysToExpire,
                    AddedUTC = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.KGrocerItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<KGrocerItemListItem> GetKGrocerItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .KGrocerItems
                        .Where(e => e.ItemID != null)
                        .Select(
                            e =>
                                new KGrocerItemListItem
                                {
                                    ItemID = e.ItemID,
                                    Name = e.Name,
                                    Price = e.Price,
                                    Description = e.Description,
                                    Category = e.Category,
                                    DaysToExpire = e.DaysToExpire,
                                    AddedUTC = e.AddedUTC
                                }
                        );

                return query.ToArray();
            }
        }

        public KGrocerItemDetail GetKGrocerItemByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .KGrocerItems
                        .Single(e => e.ItemID == id);
                return
                    new KGrocerItemDetail
                    {
                        ItemID = entity.ItemID,
                        Name = entity.Name,
                        Price = entity.Price,
                        Description = entity.Description,
                        Category = entity.Category,
                        DaysToExpire = entity.DaysToExpire,
                        AddedUTC = entity.AddedUTC,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateKGrocerItem(KGrocerItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .KGrocerItems
                        .Single(e => e.ItemID == model.ItemID);

                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Description = model.Description;
                entity.Category = model.Category;
                entity.DaysToExpire = model.DaysToExpire;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteKGrocerItem(int ItemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .KGrocerItems
                        .Single(e => e.ItemID == ItemID);

                ctx.KGrocerItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
