namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientTypeNullableInPrescreption : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Prescriptions", new[] { "PatientTypeID" });
            AlterColumn("dbo.Prescriptions", "PatientTypeID", c => c.Int());
            CreateIndex("dbo.Prescriptions", "PatientTypeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Prescriptions", new[] { "PatientTypeID" });
            AlterColumn("dbo.Prescriptions", "PatientTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Prescriptions", "PatientTypeID");
        }
    }
}
