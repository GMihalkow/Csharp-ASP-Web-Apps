namespace ShopApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_ProductId_Cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Products", "Id");
        }
    }
}
