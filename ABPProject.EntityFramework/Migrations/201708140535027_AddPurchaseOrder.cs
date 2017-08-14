namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddPurchaseOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseOrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        InventId = c.String(),
                        InventBatch = c.String(),
                        PurchCount = c.Int(nullable: false),
                        PurchPrice = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PurchaseOrderItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchId = c.String(),
                        PurchName = c.String(),
                        InventSite = c.String(),
                        InventLocation = c.String(),
                        DeliveryDate = c.DateTime(nullable: false),
                        PurchSecontractNum = c.String(),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PurchaseOrder_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PurchaseOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrderItem", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropIndex("dbo.PurchaseOrderItem", new[] { "PurchaseOrderId" });
            DropTable("dbo.PurchaseOrder",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PurchaseOrder_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PurchaseOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.PurchaseOrderItem",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PurchaseOrderItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
