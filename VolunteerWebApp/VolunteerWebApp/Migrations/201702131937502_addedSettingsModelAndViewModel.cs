namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSettingsModelAndViewModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VolunteerSettings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CanContact = c.String(),
                        CanSee = c.String(),
                        CanRefer = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Skills", "AboutSkills", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skills", "AboutSkills");
            DropTable("dbo.VolunteerSettings");
        }
    }
}
