namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedSkillModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        AnimalSkill = c.String(),
                        DisasterSkill = c.String(),
                        EducationSkill = c.String(),
                        EnviornmentSkill = c.String(),
                        HealthSkill = c.String(),
                        HumanServicesSkill = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Skills");
        }
    }
}
