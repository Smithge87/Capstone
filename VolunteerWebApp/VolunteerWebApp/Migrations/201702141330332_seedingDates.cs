namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedingDates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayNumbers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MonthNumbers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Month = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.YearNumbers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.YearNumbers");
            DropTable("dbo.MonthNumbers");
            DropTable("dbo.DayNumbers");
        }
    }
}
