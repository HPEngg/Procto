namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrescriptionMobileUploadChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrescriptionMobileImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        UploadedImage1 = c.Binary(),
                        UploadedImage2 = c.Binary(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .Index(t => t.PatientID);

            Sql("EXEC sp_executesql N' Create TRIGGER UpdatePrescriptionsUploadImages  " +
               " ON dbo.Prescriptions AFTER INSERT AS " +
" BEGIN " +
" declare @UploadedImage1 varbinary(MAX) " +
" declare @UploadedImage2 varbinary(MAX) " +
" Declare @PatientId int  " +
" Declare @Id int  " +
" select @PatientId = PatientID, @Id = Id from INSERTED  " +
" if  exists(select * from PrescriptionMobileImages where PatientID = @PatientId and  datediff(minute, DateCreated, getdate()) <= 15) " +
" begin " +
" select @UploadedImage1 = UploadedImage1, @UploadedImage2 = UploadedImage2 from PrescriptionMobileImages where PatientID = @PatientId and datediff(minute, DateCreated, getdate()) <= 15 " +
" Update Prescriptions set UploadedImage1 = @UploadedImage1, UploadedImage2 = @UploadedImage2 where ID = @Id " +
" Delete from PrescriptionMobileImages where PatientID = @PatientId and datediff(minute, DateCreated, getdate()) <= 15 " +
" end " +
" END '");


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrescriptionMobileImages", "PatientID", "dbo.Patients");
            DropIndex("dbo.PrescriptionMobileImages", new[] { "PatientID" });
            DropTable("dbo.PrescriptionMobileImages");

            Sql("EXEC sp_executesql N' DROP TRIGGER UpdatePrescriptionsUploadImages '");
        }
    }
}
