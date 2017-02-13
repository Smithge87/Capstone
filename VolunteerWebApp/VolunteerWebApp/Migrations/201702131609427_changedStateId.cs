namespace VolunteerWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedStateId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "State_ID", "dbo.States");
            DropIndex("dbo.Addresses", new[] { "State_ID" });
            DropColumn("dbo.Addresses", "StateId");
            RenameColumn(table: "dbo.Addresses", name: "State_ID", newName: "StateId");
            AlterColumn("dbo.Addresses", "StateId", c => c.Int(nullable: false));
            AlterColumn("dbo.Addresses", "StateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "StateId");
            AddForeignKey("dbo.Addresses", "StateId", "dbo.States", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "StateId", "dbo.States");
            DropIndex("dbo.Addresses", new[] { "StateId" });
            AlterColumn("dbo.Addresses", "StateId", c => c.Int());
            AlterColumn("dbo.Addresses", "StateId", c => c.String());
            RenameColumn(table: "dbo.Addresses", name: "StateId", newName: "State_ID");
            AddColumn("dbo.Addresses", "StateId", c => c.String());
            CreateIndex("dbo.Addresses", "State_ID");
            AddForeignKey("dbo.Addresses", "State_ID", "dbo.States", "ID");
        }
    }
}
