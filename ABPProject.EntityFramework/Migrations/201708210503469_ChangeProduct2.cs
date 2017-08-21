namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProduct2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProjectId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProjectId");
        }
    }
}
