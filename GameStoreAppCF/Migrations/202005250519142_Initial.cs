namespace GameStoreAppCF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Adress = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Difficulty = c.Int(nullable: false),
                        Min_Duration = c.Int(nullable: false),
                        Max_Duration = c.Int(nullable: false),
                        Min_Players = c.Int(nullable: false),
                        Max_Players = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Image = c.Binary(),
                        Author_ID = c.Int(),
                        Type_ID = c.Int(),
                        Genre_ID = c.Int(),
                        Manufacturer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genre", t => t.Genre_ID)
                .ForeignKey("dbo.Manufacturer", t => t.Manufacturer_ID)
                .ForeignKey("dbo.Type", t => t.Type_ID)
                .ForeignKey("dbo.Author", t => t.Author_ID)
                .Index(t => t.Author_ID)
                .Index(t => t.Type_ID)
                .Index(t => t.Genre_ID)
                .Index(t => t.Manufacturer_ID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Adress = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ClientID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.ClientID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Author_Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Review_Content = c.String(nullable: false),
                        Game_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Game", t => t.Game_ID)
                .Index(t => t.Game_ID);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders_Games",
                c => new
                    {
                        Game_ID = c.Int(nullable: false),
                        Order_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_ID, t.Order_ID })
                .ForeignKey("dbo.Game", t => t.Game_ID, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.Order_ID, cascadeDelete: true)
                .Index(t => t.Game_ID)
                .Index(t => t.Order_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Game", "Author_ID", "dbo.Author");
            DropForeignKey("dbo.Game", "Type_ID", "dbo.Type");
            DropForeignKey("dbo.Review", "Game_ID", "dbo.Game");
            DropForeignKey("dbo.Orders_Games", "Order_ID", "dbo.Order");
            DropForeignKey("dbo.Orders_Games", "Game_ID", "dbo.Game");
            DropForeignKey("dbo.Order", "ClientID", "dbo.Client");
            DropForeignKey("dbo.Game", "Manufacturer_ID", "dbo.Manufacturer");
            DropForeignKey("dbo.Game", "Genre_ID", "dbo.Genre");
            DropIndex("dbo.Orders_Games", new[] { "Order_ID" });
            DropIndex("dbo.Orders_Games", new[] { "Game_ID" });
            DropIndex("dbo.Review", new[] { "Game_ID" });
            DropIndex("dbo.Order", new[] { "ClientID" });
            DropIndex("dbo.Game", new[] { "Manufacturer_ID" });
            DropIndex("dbo.Game", new[] { "Genre_ID" });
            DropIndex("dbo.Game", new[] { "Type_ID" });
            DropIndex("dbo.Game", new[] { "Author_ID" });
            DropTable("dbo.Orders_Games");
            DropTable("dbo.Type");
            DropTable("dbo.Review");
            DropTable("dbo.Client");
            DropTable("dbo.Order");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Genre");
            DropTable("dbo.Game");
            DropTable("dbo.Author");
        }
    }
}
