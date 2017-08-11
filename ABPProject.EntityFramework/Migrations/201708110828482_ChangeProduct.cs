namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.Project", "LastModifierUserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "LastModifierUserId");
            DropColumn("dbo.Project", "LastModificationTime");
        }
    }
}
