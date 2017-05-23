namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BleedingOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientHistories", "BleedingMore", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientHistories", "BleedingMore", c => c.Int(nullable: false));
        }
    }
}
