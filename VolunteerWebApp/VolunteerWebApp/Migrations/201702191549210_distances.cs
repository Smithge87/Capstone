namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class distances : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Distance = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Distances");
        }
    }
}
