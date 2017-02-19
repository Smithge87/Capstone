namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedImgToInterestModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interests", "InterestLevelImgSrc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interests", "InterestLevelImgSrc");
        }
    }
}
