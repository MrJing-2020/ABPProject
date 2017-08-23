namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserTest1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AbpUsers", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbpUsers", "Test", c => c.String());
        }
    }
}
