namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedShortDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Opportunities", "AboutShort", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Opportunities", "AboutShort");
        }
    }
}
