namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLogoToOpportunity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Opportunities", "LogoSrc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Opportunities", "LogoSrc");
        }
    }
}
