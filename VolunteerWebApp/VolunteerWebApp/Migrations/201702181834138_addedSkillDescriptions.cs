namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSkillDescriptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "AboutAnimal", c => c.String());
            AddColumn("dbo.Skills", "AboutDisaster", c => c.String());
            AddColumn("dbo.Skills", "AboutEducation", c => c.String());
            AddColumn("dbo.Skills", "AboutEnviornment", c => c.String());
            AddColumn("dbo.Skills", "AboutHealth", c => c.String());
            AddColumn("dbo.Skills", "AboutHumanServices", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skills", "AboutHumanServices");
            DropColumn("dbo.Skills", "AboutHealth");
            DropColumn("dbo.Skills", "AboutEnviornment");
            DropColumn("dbo.Skills", "AboutEducation");
            DropColumn("dbo.Skills", "AboutDisaster");
            DropColumn("dbo.Skills", "AboutAnimal");
        }
    }
}
