namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddInventSite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
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
                    { "DynamicFilter_Client_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Client_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        Name = c.String(),
                        ContractNum = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Contract_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Contract_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventLocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InventSiteId = c.Int(nullable: false),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InventLocation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InventSite", t => t.InventSiteId, cascadeDelete: true)
                .Index(t => t.InventSiteId);
            
            CreateTable(
                "dbo.InventSite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        Name = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InventSite_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InventSite_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SalesOrder", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventLocation", "InventSiteId", "dbo.InventSite");
            DropIndex("dbo.InventLocation", new[] { "InventSiteId" });
            DropColumn("dbo.SalesOrder", "State");
            DropTable("dbo.InventSite",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InventSite_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InventSite_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.InventLocation",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InventLocation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Contract",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Contract_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Contract_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Client",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Client_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Client_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
