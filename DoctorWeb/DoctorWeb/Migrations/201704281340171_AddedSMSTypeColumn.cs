namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSMSTypeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMS", "SMSTypes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMS", "SMSTypes");
        }
    }
}
