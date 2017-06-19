namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSortOrderInDepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "SortOrder", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "SortOrder");
        }
    }
}
