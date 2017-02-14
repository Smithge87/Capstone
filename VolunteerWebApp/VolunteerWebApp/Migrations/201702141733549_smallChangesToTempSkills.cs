namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smallChangesToTempSkills : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SkillsNeededs", "Amount", c => c.String());
            AlterColumn("dbo.TempSkills", "Amount", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TempSkills", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.SkillsNeededs", "Amount", c => c.Int(nullable: false));
        }
    }
}
