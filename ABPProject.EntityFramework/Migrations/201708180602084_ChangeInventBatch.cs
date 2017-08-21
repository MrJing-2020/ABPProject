namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInventBatch : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventBatch", "InventBatchNum", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventBatch", "InventBatchNum", c => c.Int(nullable: false));
        }
    }
}
