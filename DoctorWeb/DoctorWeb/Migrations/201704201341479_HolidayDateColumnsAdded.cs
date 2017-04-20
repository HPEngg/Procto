namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HolidayDateColumnsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMS", "FromHolidayDate", c => c.DateTime());
            AddColumn("dbo.SMS", "ToHolidayDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMS", "ToHolidayDate");
            DropColumn("dbo.SMS", "FromHolidayDate");
        }
    }
}
