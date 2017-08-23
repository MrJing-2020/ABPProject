namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSalesOrder4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SalesOrder", "DeliveryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesOrder", "DeliveryDate", c => c.DateTime(nullable: false));
        }
    }
}
