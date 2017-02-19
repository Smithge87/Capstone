namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedImgToSkillsNeeded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SkillsNeededs", "SkillImgSrc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SkillsNeededs", "SkillImgSrc");
        }
    }
}
