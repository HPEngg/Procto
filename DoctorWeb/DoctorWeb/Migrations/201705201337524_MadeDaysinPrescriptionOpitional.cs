namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeDaysinPrescriptionOpitional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prescriptions", "Days", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prescriptions", "Days", c => c.Int(nullable: false));
        }
    }
}
