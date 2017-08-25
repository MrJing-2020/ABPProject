namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeClient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Protocol",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        FirstParty = c.String(),
                        SecondParty = c.String(),
                        ServiceContent = c.String(),
                        CostContent = c.String(),
                        FirstRightObligation = c.String(),
                        SecondRightObligation = c.String(),
                        PeriodOfValidity = c.String(),
                        DisputeSolve = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Protocol_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Protocol_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Client", "CompanyNature", c => c.String());
            AddColumn("dbo.Client", "Address", c => c.String());
            AddColumn("dbo.Client", "Linkman", c => c.String());
            AddColumn("dbo.Client", "Phone", c => c.String());
            AddColumn("dbo.Client", "RegisteredCapital", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Client", "Introduce", c => c.String());
            AddColumn("dbo.Client", "AccountBank", c => c.String());
            AddColumn("dbo.Client", "AccountId", c => c.String());
            AddColumn("dbo.Client", "TotalAssets", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Client", "NetAsset", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Client", "AnnualIncome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Client", "NetProfit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Contract", "SalesOrderId", c => c.Int(nullable: false));
            AddColumn("dbo.Contract", "Purchaser", c => c.String());
            AddColumn("dbo.Contract", "Seller", c => c.String());
            AddColumn("dbo.Contract", "GoodsStandard", c => c.String());
            AddColumn("dbo.Contract", "DeliverAddress", c => c.String());
            AddColumn("dbo.Contract", "DeliverTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contract", "DisputeSolve", c => c.String());
            AddColumn("dbo.Contract", "PeriodOfValidity", c => c.String());
            AddColumn("dbo.Contract", "AdditionalContent", c => c.String());
            AddColumn("dbo.Supplier", "CompanyNature", c => c.String());
            AddColumn("dbo.Supplier", "Address", c => c.String());
            AddColumn("dbo.Supplier", "Linkman", c => c.String());
            AddColumn("dbo.Supplier", "Phone", c => c.String());
            AddColumn("dbo.Supplier", "RegisteredCapital", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Supplier", "Introduce", c => c.String());
            AddColumn("dbo.Supplier", "AccountBank", c => c.String());
            AddColumn("dbo.Supplier", "AccountId", c => c.String());
            AddColumn("dbo.Supplier", "ProtocolId", c => c.String());
            DropColumn("dbo.Client", "Company");
            DropColumn("dbo.Client", "Department");
            DropColumn("dbo.Contract", "Name");
            DropColumn("dbo.Supplier", "Company");
            DropColumn("dbo.Supplier", "Department");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Supplier", "Department", c => c.String());
            AddColumn("dbo.Supplier", "Company", c => c.String());
            AddColumn("dbo.Contract", "Name", c => c.String());
            AddColumn("dbo.Client", "Department", c => c.String());
            AddColumn("dbo.Client", "Company", c => c.String());
            DropColumn("dbo.Supplier", "ProtocolId");
            DropColumn("dbo.Supplier", "AccountId");
            DropColumn("dbo.Supplier", "AccountBank");
            DropColumn("dbo.Supplier", "Introduce");
            DropColumn("dbo.Supplier", "RegisteredCapital");
            DropColumn("dbo.Supplier", "Phone");
            DropColumn("dbo.Supplier", "Linkman");
            DropColumn("dbo.Supplier", "Address");
            DropColumn("dbo.Supplier", "CompanyNature");
            DropColumn("dbo.Contract", "AdditionalContent");
            DropColumn("dbo.Contract", "PeriodOfValidity");
            DropColumn("dbo.Contract", "DisputeSolve");
            DropColumn("dbo.Contract", "DeliverTime");
            DropColumn("dbo.Contract", "DeliverAddress");
            DropColumn("dbo.Contract", "GoodsStandard");
            DropColumn("dbo.Contract", "Seller");
            DropColumn("dbo.Contract", "Purchaser");
            DropColumn("dbo.Contract", "SalesOrderId");
            DropColumn("dbo.Client", "NetProfit");
            DropColumn("dbo.Client", "AnnualIncome");
            DropColumn("dbo.Client", "NetAsset");
            DropColumn("dbo.Client", "TotalAssets");
            DropColumn("dbo.Client", "AccountId");
            DropColumn("dbo.Client", "AccountBank");
            DropColumn("dbo.Client", "Introduce");
            DropColumn("dbo.Client", "RegisteredCapital");
            DropColumn("dbo.Client", "Phone");
            DropColumn("dbo.Client", "Linkman");
            DropColumn("dbo.Client", "Address");
            DropColumn("dbo.Client", "CompanyNature");
            DropTable("dbo.Protocol",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Protocol_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Protocol_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
