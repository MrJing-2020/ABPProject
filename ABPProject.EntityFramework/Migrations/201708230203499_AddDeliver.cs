namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeliver : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BankAccouts", newName: "BankAccout");
            CreateTable(
                "dbo.Deliver",
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
                        DeliverTime = c.DateTime(nullable: false),
                        SalesOrderId = c.Int(nullable: false),
                        LogisticsCompany = c.String(),
                        LogisticsNum = c.String(),
                        Remark = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Deliver_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Deliver_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Deliver",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Deliver_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Deliver_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            RenameTable(name: "dbo.BankAccout", newName: "BankAccouts");
        }
    }
}
