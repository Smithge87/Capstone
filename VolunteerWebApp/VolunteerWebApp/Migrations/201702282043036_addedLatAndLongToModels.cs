namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLatAndLongToModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Opportunities", "GeoLat", c => c.String());
            AddColumn("dbo.Opportunities", "GeoLong", c => c.String());
            AddColumn("dbo.AspNetUsers", "GeoLat", c => c.String());
            AddColumn("dbo.AspNetUsers", "GeoLong", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "GeoLong");
            DropColumn("dbo.AspNetUsers", "GeoLat");
            DropColumn("dbo.Opportunities", "GeoLong");
            DropColumn("dbo.Opportunities", "GeoLat");
        }
    }
}
