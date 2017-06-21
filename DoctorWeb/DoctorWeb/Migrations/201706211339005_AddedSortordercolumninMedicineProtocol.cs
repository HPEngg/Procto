namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSortordercolumninMedicineProtocol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrescriptionCategories", "SortOrder", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrescriptionCategories", "SortOrder");
        }
    }
}
