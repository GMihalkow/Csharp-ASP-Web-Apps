namespace ShopApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "IsCompleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IsCompleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "Status");
        }
    }
}
