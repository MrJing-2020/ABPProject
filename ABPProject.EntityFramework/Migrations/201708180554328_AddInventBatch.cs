namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddInventBatch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventBatch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        ProductId = c.Int(nullable: false),
                        InventBatchNum = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InventBatch_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InventBatch_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventBatch", "ProductId", "dbo.Product");
            DropIndex("dbo.InventBatch", new[] { "ProductId" });
            DropTable("dbo.InventBatch",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InventBatch_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InventBatch_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
