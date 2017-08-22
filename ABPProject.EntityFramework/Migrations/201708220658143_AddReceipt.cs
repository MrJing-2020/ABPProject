namespace ABPProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceipt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receipt",
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
                        ClientId = c.Int(nullable: false),
                        SalesOrderId = c.Int(nullable: false),
                        ReceiptWay = c.String(),
                        PaymentMethod = c.String(),
                        ReceiptDate = c.DateTime(nullable: false),
                        JournalName = c.String(),
                        PostingProfile = c.String(),
                        BankName = c.String(),
                        JournalBalanceNum = c.String(),
                        JournalBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LineAmountCaps = c.String(),
                        BankTransactionNum = c.String(),
                        SupplyOfGoods = c.String(),
                        ContractNum = c.String(),
                        Remark = c.String(),
                        Currency = c.String(),
                        ReceiptAbstract = c.String(),
                        State = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Receipt_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Receipt_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Receipt",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Receipt_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Receipt_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
