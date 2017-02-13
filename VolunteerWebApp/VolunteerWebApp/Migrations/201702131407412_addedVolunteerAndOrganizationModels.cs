namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedVolunteerAndOrganizationModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "OrganizationName", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserTitle");
            DropColumn("dbo.AspNetUsers", "OrganizationName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
