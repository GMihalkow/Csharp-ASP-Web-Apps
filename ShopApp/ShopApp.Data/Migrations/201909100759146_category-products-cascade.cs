namespace ShopApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryproductscascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id");
        }
    }
}
