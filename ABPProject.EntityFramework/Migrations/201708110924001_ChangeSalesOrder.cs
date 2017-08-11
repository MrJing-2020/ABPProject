namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSalesOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesOrder", "DistributionMode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesOrder", "DistributionMode");
        }
    }
}
