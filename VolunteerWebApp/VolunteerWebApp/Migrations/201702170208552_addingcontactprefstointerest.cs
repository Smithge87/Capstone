namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcontactprefstointerest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interests", "CanShow", c => c.Boolean(nullable: false));
            AddColumn("dbo.Interests", "CanContact", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interests", "CanContact");
            DropColumn("dbo.Interests", "CanShow");
        }
    }
}
