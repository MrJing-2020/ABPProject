namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePurchaseOrderItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseOrderItem", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchaseOrderItem", "InventBatchId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrderItem", "InventBatchId", c => c.String());
            AlterColumn("dbo.PurchaseOrderItem", "ProductId", c => c.String());
        }
    }
}
