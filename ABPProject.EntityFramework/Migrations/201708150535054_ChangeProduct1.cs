namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProduct1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Category", c => c.String());
            AddColumn("dbo.Product", "NameAlias", c => c.String());
            AddColumn("dbo.Product", "InventoryUnit", c => c.String());
            AddColumn("dbo.Product", "PurchaseUnit", c => c.String());
            AddColumn("dbo.Product", "SalesUnit", c => c.String());
            AddColumn("dbo.Product", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Stopped", c => c.Boolean(nullable: false));
            DropColumn("dbo.Product", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.Product", "Stopped");
            DropColumn("dbo.Product", "DepartmentId");
            DropColumn("dbo.Product", "CompanyId");
            DropColumn("dbo.Product", "SalesUnit");
            DropColumn("dbo.Product", "PurchaseUnit");
            DropColumn("dbo.Product", "InventoryUnit");
            DropColumn("dbo.Product", "NameAlias");
            DropColumn("dbo.Product", "Category");
        }
    }
}
