namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddSalesOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesOrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalesOrderId = c.Int(nullable: false),
                        Inventid = c.String(),
                        InventBatch = c.String(),
                        PurchCount = c.Int(nullable: false),
                        PurchPrice = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SalesOrderItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SalesOrder", t => t.SalesOrderId, cascadeDelete: true)
                .Index(t => t.SalesOrderId);
            
            CreateTable(
                "dbo.SalesOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalesId = c.String(),
                        SalesName = c.String(),
                        ClientId = c.String(),
                        InventSite = c.String(),
                        InventLocation = c.String(),
                        SalesContractNum = c.String(),
                        DeliveryDate = c.DateTime(nullable: false),
                        Consignee = c.String(),
                        DeliveryAddress = c.String(),
                        PostCode = c.String(),
                        MobilePhone = c.String(),
                        InvoiceHeader = c.String(),
                        Instructions = c.String(),
                        PaymentMethod = c.String(),
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
                    { "DynamicFilter_SalesOrder_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SalesOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesOrderItems", "SalesOrderId", "dbo.SalesOrder");
            DropIndex("dbo.SalesOrderItems", new[] { "SalesOrderId" });
            DropTable("dbo.SalesOrder",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SalesOrder_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SalesOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.SalesOrderItems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SalesOrderItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
