namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHoliday2Columns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMS", "FromHolidayDate2", c => c.DateTime());
            AddColumn("dbo.SMS", "ToHolidayDate2", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMS", "ToHolidayDate2");
            DropColumn("dbo.SMS", "FromHolidayDate2");
        }
    }
}
