namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedSearchViewModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Opportunities", "GeoLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Opportunities", "GeoLocation", c => c.String());
        }
    }
}
