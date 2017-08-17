namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ChangPurchase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(),
                        NameAlias = c.String(),
                        Company = c.String(),
                        Department = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Supplier_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Supplier_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PurchaseOrder", "PurchNum", c => c.String());
            AddColumn("dbo.PurchaseOrder", "SupplierId", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrder", "InventSiteId", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrder", "InventLocationId", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrder", "ContractId", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrder", "State", c => c.Int(nullable: false));
            AddColumn("dbo.SalesOrder", "SalesNum", c => c.String());
            AddColumn("dbo.SalesOrder", "InventSiteId", c => c.Int(nullable: false));
            AddColumn("dbo.SalesOrder", "InventLocationId", c => c.Int(nullable: false));
            AddColumn("dbo.SalesOrder", "SalesContractId", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseOrder", "PurchId");
            DropColumn("dbo.PurchaseOrder", "PurchName");
            DropColumn("dbo.PurchaseOrder", "InventSite");
            DropColumn("dbo.PurchaseOrder", "InventLocation");
            DropColumn("dbo.PurchaseOrder", "PurchSecontractNum");
            DropColumn("dbo.SalesOrder", "SalesId");
            DropColumn("dbo.SalesOrder", "InventSite");
            DropColumn("dbo.SalesOrder", "InventLocation");
            DropColumn("dbo.SalesOrder", "SalesContractNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesOrder", "SalesContractNum", c => c.String());
            AddColumn("dbo.SalesOrder", "InventLocation", c => c.String());
            AddColumn("dbo.SalesOrder", "InventSite", c => c.String());
            AddColumn("dbo.SalesOrder", "SalesId", c => c.String());
            AddColumn("dbo.PurchaseOrder", "PurchSecontractNum", c => c.String());
            AddColumn("dbo.PurchaseOrder", "InventLocation", c => c.String());
            AddColumn("dbo.PurchaseOrder", "InventSite", c => c.String());
            AddColumn("dbo.PurchaseOrder", "PurchName", c => c.String());
            AddColumn("dbo.PurchaseOrder", "PurchId", c => c.String());
            DropColumn("dbo.SalesOrder", "SalesContractId");
            DropColumn("dbo.SalesOrder", "InventLocationId");
            DropColumn("dbo.SalesOrder", "InventSiteId");
            DropColumn("dbo.SalesOrder", "SalesNum");
            DropColumn("dbo.PurchaseOrder", "State");
            DropColumn("dbo.PurchaseOrder", "ContractId");
            DropColumn("dbo.PurchaseOrder", "InventLocationId");
            DropColumn("dbo.PurchaseOrder", "InventSiteId");
            DropColumn("dbo.PurchaseOrder", "SupplierId");
            DropColumn("dbo.PurchaseOrder", "PurchNum");
            DropTable("dbo.Supplier",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Supplier_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Supplier_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
