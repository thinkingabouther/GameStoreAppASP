namespace GameStoreAppCF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blablabla : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageForGameViewModels",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        GameID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageForGameViewModels");
        }
    }
}
