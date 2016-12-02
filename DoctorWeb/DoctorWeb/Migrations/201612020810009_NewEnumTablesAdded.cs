namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEnumTablesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dosages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Dozs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OINTTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Medicines", "OINTTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "MorningDozID", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "NoonDozID", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "NightDozID", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "DosageID", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "IsDayAffected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Medicines", "Morning_ID", c => c.Int());
            AddColumn("dbo.Medicines", "Night_ID", c => c.Int());
            AddColumn("dbo.Medicines", "Noon_ID", c => c.Int());
            AddColumn("dbo.PrescriptionMedicines", "OINTTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "MorningDozID", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "NoonDozID", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "NightDozID", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "DosageID", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "Morning_ID", c => c.Int());
            AddColumn("dbo.PrescriptionMedicines", "Night_ID", c => c.Int());
            AddColumn("dbo.PrescriptionMedicines", "Noon_ID", c => c.Int());
            CreateIndex("dbo.Medicines", "OINTTypeID");
            CreateIndex("dbo.Medicines", "DosageID");
            CreateIndex("dbo.Medicines", "Morning_ID");
            CreateIndex("dbo.Medicines", "Night_ID");
            CreateIndex("dbo.Medicines", "Noon_ID");
            CreateIndex("dbo.PrescriptionMedicines", "OINTTypeID");
            CreateIndex("dbo.PrescriptionMedicines", "DosageID");
            CreateIndex("dbo.PrescriptionMedicines", "Morning_ID");
            CreateIndex("dbo.PrescriptionMedicines", "Night_ID");
            CreateIndex("dbo.PrescriptionMedicines", "Noon_ID");
            AddForeignKey("dbo.Medicines", "DosageID", "dbo.Dosages", "ID");
            AddForeignKey("dbo.Medicines", "Morning_ID", "dbo.Dozs", "ID");
            AddForeignKey("dbo.Medicines", "Night_ID", "dbo.Dozs", "ID");
            AddForeignKey("dbo.Medicines", "Noon_ID", "dbo.Dozs", "ID");
            AddForeignKey("dbo.Medicines", "OINTTypeID", "dbo.OINTTypes", "ID");
            AddForeignKey("dbo.PrescriptionMedicines", "DosageID", "dbo.Dosages", "ID");
            AddForeignKey("dbo.PrescriptionMedicines", "Morning_ID", "dbo.Dozs", "ID");
            AddForeignKey("dbo.PrescriptionMedicines", "Night_ID", "dbo.Dozs", "ID");
            AddForeignKey("dbo.PrescriptionMedicines", "Noon_ID", "dbo.Dozs", "ID");
            AddForeignKey("dbo.PrescriptionMedicines", "OINTTypeID", "dbo.OINTTypes", "ID");
            DropColumn("dbo.Medicines", "OINT");
            DropColumn("dbo.Medicines", "Morning");
            DropColumn("dbo.Medicines", "Noon");
            DropColumn("dbo.Medicines", "Night");
            DropColumn("dbo.Medicines", "DozTiming");
            DropColumn("dbo.PrescriptionMedicines", "OINT");
            DropColumn("dbo.PrescriptionMedicines", "Morning");
            DropColumn("dbo.PrescriptionMedicines", "Noon");
            DropColumn("dbo.PrescriptionMedicines", "Night");
            DropColumn("dbo.PrescriptionMedicines", "DozTiming");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrescriptionMedicines", "DozTiming", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "Night", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "Noon", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "Morning", c => c.Int(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "OINT", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "DozTiming", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "Night", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "Noon", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "Morning", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "OINT", c => c.Int(nullable: false));
            DropForeignKey("dbo.PrescriptionMedicines", "OINTTypeID", "dbo.OINTTypes");
            DropForeignKey("dbo.PrescriptionMedicines", "Noon_ID", "dbo.Dozs");
            DropForeignKey("dbo.PrescriptionMedicines", "Night_ID", "dbo.Dozs");
            DropForeignKey("dbo.PrescriptionMedicines", "Morning_ID", "dbo.Dozs");
            DropForeignKey("dbo.PrescriptionMedicines", "DosageID", "dbo.Dosages");
            DropForeignKey("dbo.Medicines", "OINTTypeID", "dbo.OINTTypes");
            DropForeignKey("dbo.Medicines", "Noon_ID", "dbo.Dozs");
            DropForeignKey("dbo.Medicines", "Night_ID", "dbo.Dozs");
            DropForeignKey("dbo.Medicines", "Morning_ID", "dbo.Dozs");
            DropForeignKey("dbo.Medicines", "DosageID", "dbo.Dosages");
            DropIndex("dbo.PrescriptionMedicines", new[] { "Noon_ID" });
            DropIndex("dbo.PrescriptionMedicines", new[] { "Night_ID" });
            DropIndex("dbo.PrescriptionMedicines", new[] { "Morning_ID" });
            DropIndex("dbo.PrescriptionMedicines", new[] { "DosageID" });
            DropIndex("dbo.PrescriptionMedicines", new[] { "OINTTypeID" });
            DropIndex("dbo.Medicines", new[] { "Noon_ID" });
            DropIndex("dbo.Medicines", new[] { "Night_ID" });
            DropIndex("dbo.Medicines", new[] { "Morning_ID" });
            DropIndex("dbo.Medicines", new[] { "DosageID" });
            DropIndex("dbo.Medicines", new[] { "OINTTypeID" });
            DropColumn("dbo.PrescriptionMedicines", "Noon_ID");
            DropColumn("dbo.PrescriptionMedicines", "Night_ID");
            DropColumn("dbo.PrescriptionMedicines", "Morning_ID");
            DropColumn("dbo.PrescriptionMedicines", "DosageID");
            DropColumn("dbo.PrescriptionMedicines", "NightDozID");
            DropColumn("dbo.PrescriptionMedicines", "NoonDozID");
            DropColumn("dbo.PrescriptionMedicines", "MorningDozID");
            DropColumn("dbo.PrescriptionMedicines", "OINTTypeID");
            DropColumn("dbo.Medicines", "Noon_ID");
            DropColumn("dbo.Medicines", "Night_ID");
            DropColumn("dbo.Medicines", "Morning_ID");
            DropColumn("dbo.Medicines", "IsDayAffected");
            DropColumn("dbo.Medicines", "DosageID");
            DropColumn("dbo.Medicines", "NightDozID");
            DropColumn("dbo.Medicines", "NoonDozID");
            DropColumn("dbo.Medicines", "MorningDozID");
            DropColumn("dbo.Medicines", "OINTTypeID");
            DropTable("dbo.OINTTypes");
            DropTable("dbo.Dozs");
            DropTable("dbo.Dosages");
        }
    }
}
