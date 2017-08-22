namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePurchaseOrder1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderItem", "InventBatchId", c => c.String());
            DropColumn("dbo.PurchaseOrderItem", "InventBatch");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderItem", "InventBatch", c => c.String());
            DropColumn("dbo.PurchaseOrderItem", "InventBatchId");
        }
    }
}
