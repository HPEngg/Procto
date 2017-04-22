namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvestigationMultiselectionAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prescriptions", "InvestigationID", "dbo.Investigations");
            DropIndex("dbo.Prescriptions", new[] { "InvestigationID" });
            CreateTable(
                "dbo.InvestigationPrescriptions",
                c => new
                    {
                        Investigation_ID = c.Int(nullable: false),
                        Prescription_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Investigation_ID, t.Prescription_ID })
                .ForeignKey("dbo.Investigations", t => t.Investigation_ID)
                .ForeignKey("dbo.Prescriptions", t => t.Prescription_ID)
                .Index(t => t.Investigation_ID)
                .Index(t => t.Prescription_ID);
            
            DropColumn("dbo.Prescriptions", "InvestigationID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prescriptions", "InvestigationID", c => c.Int(nullable: false));
            DropForeignKey("dbo.InvestigationPrescriptions", "Prescription_ID", "dbo.Prescriptions");
            DropForeignKey("dbo.InvestigationPrescriptions", "Investigation_ID", "dbo.Investigations");
            DropIndex("dbo.InvestigationPrescriptions", new[] { "Prescription_ID" });
            DropIndex("dbo.InvestigationPrescriptions", new[] { "Investigation_ID" });
            DropTable("dbo.InvestigationPrescriptions");
            CreateIndex("dbo.Prescriptions", "InvestigationID");
            AddForeignKey("dbo.Prescriptions", "InvestigationID", "dbo.Investigations", "ID");
        }
    }
}
