namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSalesOrder3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesOrder", "ClientId", c => c.Int(nullable: false));
            DropColumn("dbo.SalesOrder", "SalesName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesOrder", "SalesName", c => c.String());
            AlterColumn("dbo.SalesOrder", "ClientId", c => c.String());
        }
    }
}
