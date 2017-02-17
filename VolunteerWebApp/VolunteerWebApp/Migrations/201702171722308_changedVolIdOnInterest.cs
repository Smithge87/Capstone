namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedVolIdOnInterest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Interests", "VolunteerId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interests", "VolunteerId", c => c.Int(nullable: false));
        }
    }
}
