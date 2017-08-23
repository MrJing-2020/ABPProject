namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "Test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "Test");
        }
    }
}
