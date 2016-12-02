namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEnumTablesAdded2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Medicines", new[] { "Morning_ID" });
            DropIndex("dbo.Medicines", new[] { "Night_ID" });
            DropIndex("dbo.Medicines", new[] { "Noon_ID" });
            DropColumn("dbo.Medicines", "MorningDozID");
            DropColumn("dbo.Medicines", "NightDozID");
            DropColumn("dbo.Medicines", "NoonDozID");
            RenameColumn(table: "dbo.Medicines", name: "Morning_ID", newName: "MorningDozID");
            RenameColumn(table: "dbo.Medicines", name: "Night_ID", newName: "NightDozID");
            RenameColumn(table: "dbo.Medicines", name: "Noon_ID", newName: "NoonDozID");
            AddColumn("dbo.Medicines", "Doz_ID", c => c.Int());
            AddColumn("dbo.Medicines", "Doz_ID1", c => c.Int());
            AddColumn("dbo.Medicines", "Doz_ID2", c => c.Int());
            AlterColumn("dbo.Medicines", "MorningDozID", c => c.Int(nullable: false));
            AlterColumn("dbo.Medicines", "NightDozID", c => c.Int(nullable: false));
            AlterColumn("dbo.Medicines", "NoonDozID", c => c.Int(nullable: false));
            CreateIndex("dbo.Medicines", "MorningDozID");
            CreateIndex("dbo.Medicines", "NoonDozID");
            CreateIndex("dbo.Medicines", "NightDozID");
            CreateIndex("dbo.Medicines", "Doz_ID");
            CreateIndex("dbo.Medicines", "Doz_ID1");
            CreateIndex("dbo.Medicines", "Doz_ID2");
            AddForeignKey("dbo.Medicines", "Doz_ID", "dbo.Dozs", "ID");
            AddForeignKey("dbo.Medicines", "Doz_ID1", "dbo.Dozs", "ID");
            AddForeignKey("dbo.Medicines", "Doz_ID2", "dbo.Dozs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicines", "Doz_ID2", "dbo.Dozs");
            DropForeignKey("dbo.Medicines", "Doz_ID1", "dbo.Dozs");
            DropForeignKey("dbo.Medicines", "Doz_ID", "dbo.Dozs");
            DropIndex("dbo.Medicines", new[] { "Doz_ID2" });
            DropIndex("dbo.Medicines", new[] { "Doz_ID1" });
            DropIndex("dbo.Medicines", new[] { "Doz_ID" });
            DropIndex("dbo.Medicines", new[] { "NightDozID" });
            DropIndex("dbo.Medicines", new[] { "NoonDozID" });
            DropIndex("dbo.Medicines", new[] { "MorningDozID" });
            AlterColumn("dbo.Medicines", "NoonDozID", c => c.Int());
            AlterColumn("dbo.Medicines", "NightDozID", c => c.Int());
            AlterColumn("dbo.Medicines", "MorningDozID", c => c.Int());
            DropColumn("dbo.Medicines", "Doz_ID2");
            DropColumn("dbo.Medicines", "Doz_ID1");
            DropColumn("dbo.Medicines", "Doz_ID");
            RenameColumn(table: "dbo.Medicines", name: "NoonDozID", newName: "Noon_ID");
            RenameColumn(table: "dbo.Medicines", name: "NightDozID", newName: "Night_ID");
            RenameColumn(table: "dbo.Medicines", name: "MorningDozID", newName: "Morning_ID");
            AddColumn("dbo.Medicines", "NoonDozID", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "NightDozID", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "MorningDozID", c => c.Int(nullable: false));
            CreateIndex("dbo.Medicines", "Noon_ID");
            CreateIndex("dbo.Medicines", "Night_ID");
            CreateIndex("dbo.Medicines", "Morning_ID");
        }
    }
}
