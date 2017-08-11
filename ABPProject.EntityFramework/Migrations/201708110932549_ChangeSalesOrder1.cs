namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSalesOrder1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SalesOrderItems", "InventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesOrderItems", "InventId", c => c.String());
        }
    }
}
