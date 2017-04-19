namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnUploadedImage2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescriptions", "UploadedImage1", c => c.Binary());
            AddColumn("dbo.Prescriptions", "UploadedImage2", c => c.Binary());
            DropColumn("dbo.Prescriptions", "UploadedImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prescriptions", "UploadedImage", c => c.Binary());
            DropColumn("dbo.Prescriptions", "UploadedImage2");
            DropColumn("dbo.Prescriptions", "UploadedImage1");
        }
    }
}
