namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OtherColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescriptions", "Other", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescriptions", "Other");
        }
    }
}
