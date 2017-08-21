namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSalesItem2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesOrderItems", "ProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesOrderItems", "ProductId", c => c.String());
        }
    }
}
