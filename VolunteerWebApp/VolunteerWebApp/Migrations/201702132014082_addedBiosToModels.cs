namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedBiosToModels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Addresses", newName: "Information");
            AddColumn("dbo.Information", "AboutInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "AboutInfo");
            RenameTable(name: "dbo.Information", newName: "Addresses");
        }
    }
}
