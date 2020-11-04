namespace KMarket.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderMeal",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OwnerID = c.Guid(nullable: false),
                        LastModifiedID = c.Guid(nullable: false),
                        MealID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        AddedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.KCafeMeal", t => t.MealID, cascadeDelete: true)
                .Index(t => t.MealID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderMeal", "MealID", "dbo.KCafeMeal");
            DropIndex("dbo.OrderMeal", new[] { "MealID" });
            DropTable("dbo.OrderMeal");
        }
    }
}
