namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "AXProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Project", "BeginDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "Risk", c => c.String());
            AddColumn("dbo.Project", "BuyMethod", c => c.String());
            AddColumn("dbo.Project", "RedeemMethod", c => c.String());
            AddColumn("dbo.Project", "Yield", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "Yield");
            DropColumn("dbo.Project", "RedeemMethod");
            DropColumn("dbo.Project", "BuyMethod");
            DropColumn("dbo.Project", "Risk");
            DropColumn("dbo.Project", "BeginDate");
            DropColumn("dbo.Project", "AXProjectId");
        }
    }
}
