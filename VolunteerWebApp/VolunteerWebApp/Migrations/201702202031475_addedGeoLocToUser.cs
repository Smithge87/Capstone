namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedGeoLocToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LogoImgSrc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LogoImgSrc");
        }
    }
}
