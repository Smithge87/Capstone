namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedInterests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OpportunityId = c.Int(nullable: false),
                        VolunteerId = c.Int(nullable: false),
                        InterestLevel = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Interests");
        }
    }
}
