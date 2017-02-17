namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedSettingsToBools : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interests", "VolunteerUserName", c => c.String());
            AlterColumn("dbo.VolunteerSettings", "CanContact", c => c.Boolean(nullable: false));
            AlterColumn("dbo.VolunteerSettings", "CanSee", c => c.Boolean(nullable: false));
            AlterColumn("dbo.VolunteerSettings", "CanRefer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VolunteerSettings", "CanRefer", c => c.String());
            AlterColumn("dbo.VolunteerSettings", "CanSee", c => c.String());
            AlterColumn("dbo.VolunteerSettings", "CanContact", c => c.String());
            DropColumn("dbo.Interests", "VolunteerUserName");
        }
    }
}
