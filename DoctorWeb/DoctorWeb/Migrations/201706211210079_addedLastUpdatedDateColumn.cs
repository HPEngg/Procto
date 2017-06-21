namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLastUpdatedDateColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "LastUpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "LastUpdatedDate");
        }
    }
}
