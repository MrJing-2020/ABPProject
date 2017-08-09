namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Description");
        }
    }
}
