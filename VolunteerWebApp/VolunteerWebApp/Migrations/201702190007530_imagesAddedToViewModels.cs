namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagesAddedToViewModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TempSkills", "SkillImgSrc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TempSkills", "SkillImgSrc");
        }
    }
}
