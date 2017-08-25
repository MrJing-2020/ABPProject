namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSupplier : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Supplier", "ProtocolId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Supplier", "ProtocolId", c => c.String());
        }
    }
}
