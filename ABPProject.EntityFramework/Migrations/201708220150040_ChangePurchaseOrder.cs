namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePurchaseOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderItem", "ProductId", c => c.String());
            DropColumn("dbo.PurchaseOrderItem", "InventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderItem", "InventId", c => c.String());
            DropColumn("dbo.PurchaseOrderItem", "ProductId");
        }
    }
}
