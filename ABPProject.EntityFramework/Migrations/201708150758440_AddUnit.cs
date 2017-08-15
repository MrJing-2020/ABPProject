namespace ABPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnitOfMeasureTranslation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UnitOfMeasureTranslation");
        }
    }
}
