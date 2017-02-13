namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAddressModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        StateId = c.String(),
                        Zipcode = c.String(),
                        State_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.States", t => t.State_ID)
                .Index(t => t.State_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "State_ID", "dbo.States");
            DropIndex("dbo.Addresses", new[] { "State_ID" });
            DropTable("dbo.Addresses");
        }
    }
}
