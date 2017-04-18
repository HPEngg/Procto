namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUnitAndTotalColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrescriptionMedicines", "Unit", c => c.Single(nullable: false));
            AddColumn("dbo.PrescriptionMedicines", "Total", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrescriptionMedicines", "Total");
            DropColumn("dbo.PrescriptionMedicines", "Unit");
        }
    }
}
