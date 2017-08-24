namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientPayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        SalesOrderId = c.Int(nullable: false),
                        PaymentBank = c.String(),
                        BankTransactionNum = c.String(),
                        PaymentSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReceiptBank = c.String(),
                        ReceiptAccountId = c.String(),
                        State = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ClientPayment_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ClientPayment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClientPayments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ClientPayment_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ClientPayment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
