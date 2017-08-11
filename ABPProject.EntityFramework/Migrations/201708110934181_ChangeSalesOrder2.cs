namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSalesOrder2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesOrderItems", "InventId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesOrderItems", "InventId");
        }
    }
}
