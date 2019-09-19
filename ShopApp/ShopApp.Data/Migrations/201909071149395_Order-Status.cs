namespace ShopApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsCompleted");
        }
    }
}
