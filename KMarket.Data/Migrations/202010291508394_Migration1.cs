namespace KMarket.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KGrocerItem",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Description = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        DaysToExpire = c.Int(nullable: false),
                        AddedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KGrocerItem");
        }
    }
}
