namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSalesItem1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesOrderItems", "ProductId", c => c.String());
            DropColumn("dbo.SalesOrderItems", "InventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesOrderItems", "InventId", c => c.String());
            DropColumn("dbo.SalesOrderItems", "ProductId");
        }
    }
}
