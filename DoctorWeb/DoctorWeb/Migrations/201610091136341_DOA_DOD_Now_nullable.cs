namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DOA_DOD_Now_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientHistories", "DOA", c => c.DateTime());
            AlterColumn("dbo.PatientHistories", "DOD", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientHistories", "DOD", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PatientHistories", "DOA", c => c.DateTime(nullable: false));
        }
    }
}
