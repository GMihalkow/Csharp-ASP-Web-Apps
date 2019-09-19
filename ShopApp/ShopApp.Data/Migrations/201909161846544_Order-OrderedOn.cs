namespace ShopApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderOrderedOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderedOn");
        }
    }
}
