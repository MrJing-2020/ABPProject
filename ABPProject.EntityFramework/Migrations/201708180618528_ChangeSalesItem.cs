namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSalesItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesOrderItems", "InventBatchId", c => c.Int(nullable: false));
            DropColumn("dbo.SalesOrderItems", "InventBatch");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesOrderItems", "InventBatch", c => c.String());
            DropColumn("dbo.SalesOrderItems", "InventBatchId");
        }
    }
}
