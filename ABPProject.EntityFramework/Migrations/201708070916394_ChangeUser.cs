namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpUsers", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpUsers", "Surname", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
