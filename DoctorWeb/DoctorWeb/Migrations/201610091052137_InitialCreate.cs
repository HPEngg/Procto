namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Charges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PaymentTypeID = c.Int(nullable: false),
                        PrescriptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeID)
                .ForeignKey("dbo.Prescriptions", t => t.PrescriptionID)
                .Index(t => t.PaymentTypeID)
                .Index(t => t.PrescriptionID);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PaymentTypeName = c.String(),
                        Rupees = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Days = c.Int(nullable: false),
                        Diagnosis = c.String(),
                        Procedure = c.String(),
                        Date = c.DateTime(nullable: false),
                        FollowDate = c.DateTime(nullable: false),
                        M = c.String(),
                        Percent = c.Int(nullable: false),
                        Less = c.String(),
                        Rs = c.String(),
                        Received = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pending = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DoctorID = c.Int(nullable: false),
                        PatientID = c.Int(nullable: false),
                        InstructionID = c.Int(nullable: false),
                        PatientTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .ForeignKey("dbo.Instructions", t => t.InstructionID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .ForeignKey("dbo.PatientTypes", t => t.PatientTypeID)
                .Index(t => t.DoctorID)
                .Index(t => t.PatientID)
                .Index(t => t.InstructionID)
                .Index(t => t.PatientTypeID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Contact = c.String(),
                        DoctorType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Instructions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                        ReferredBy = c.Int(nullable: false),
                        Relative = c.String(),
                        DepartmentID = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Contact = c.String(),
                        Email = c.String(),
                        Occupation = c.String(),
                        Habit = c.Int(nullable: false),
                        FoodPreference = c.Int(nullable: false),
                        RemindMeAbout = c.String(),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.PatientTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PatientTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OINT = c.Int(nullable: false),
                        OINTMore = c.String(),
                        Morning = c.Int(nullable: false),
                        Noon = c.Int(nullable: false),
                        Night = c.Int(nullable: false),
                        DozTiming = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        PrescriptionCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PrescriptionCategories", t => t.PrescriptionCategoryID)
                .Index(t => t.PrescriptionCategoryID);
            
            CreateTable(
                "dbo.PrescriptionCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Offlines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExecutedAt = c.DateTime(nullable: false),
                        Query = c.String(),
                        IsExecuted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PatientHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RP = c.String(),
                        KCO = c.String(),
                        CO = c.String(),
                        ComplainForm = c.String(),
                        Constipation = c.Int(nullable: false),
                        ConstipationMore = c.String(),
                        Pain = c.Int(nullable: false),
                        PainMore = c.String(),
                        Burning = c.Int(nullable: false),
                        BurningMore = c.String(),
                        Bleeding = c.Int(nullable: false),
                        BleedingMore = c.Int(nullable: false),
                        Itching = c.Int(nullable: false),
                        ItchingMore = c.String(),
                        PusDrainage = c.Int(nullable: false),
                        PusDrainageMore = c.String(),
                        Swelling = c.Int(nullable: false),
                        SwellingMore = c.String(),
                        SCO = c.String(),
                        ACO = c.String(),
                        Allergy = c.String(),
                        History = c.String(),
                        Weight = c.Single(nullable: false),
                        Height = c.Single(nullable: false),
                        T = c.Int(nullable: false),
                        PR = c.String(),
                        BP = c.Int(nullable: false),
                        SPO2 = c.String(),
                        PRR = c.String(),
                        Proctoscopy = c.String(),
                        LightOnOff = c.Boolean(nullable: false),
                        Other = c.String(),
                        DOA = c.DateTime(nullable: false),
                        DOD = c.DateTime(nullable: false),
                        Dignosis = c.String(),
                        Procedure = c.String(),
                        Comment = c.String(),
                        PatientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Header = c.Binary(),
                        Footer = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PrescriptionMedicines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OINT = c.Int(nullable: false),
                        OINTMore = c.String(),
                        Morning = c.Int(nullable: false),
                        Noon = c.Int(nullable: false),
                        Night = c.Int(nullable: false),
                        DozTiming = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        PrescriptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Prescriptions", t => t.PrescriptionID)
                .Index(t => t.PrescriptionID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PrescriptionMedicines", "PrescriptionID", "dbo.Prescriptions");
            DropForeignKey("dbo.PatientHistories", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Medicines", "PrescriptionCategoryID", "dbo.PrescriptionCategories");
            DropForeignKey("dbo.Charges", "PrescriptionID", "dbo.Prescriptions");
            DropForeignKey("dbo.Prescriptions", "PatientTypeID", "dbo.PatientTypes");
            DropForeignKey("dbo.Prescriptions", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Patients", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Prescriptions", "InstructionID", "dbo.Instructions");
            DropForeignKey("dbo.Prescriptions", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Charges", "PaymentTypeID", "dbo.PaymentTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PrescriptionMedicines", new[] { "PrescriptionID" });
            DropIndex("dbo.PatientHistories", new[] { "PatientID" });
            DropIndex("dbo.Medicines", new[] { "PrescriptionCategoryID" });
            DropIndex("dbo.Patients", new[] { "DoctorID" });
            DropIndex("dbo.Prescriptions", new[] { "PatientTypeID" });
            DropIndex("dbo.Prescriptions", new[] { "InstructionID" });
            DropIndex("dbo.Prescriptions", new[] { "PatientID" });
            DropIndex("dbo.Prescriptions", new[] { "DoctorID" });
            DropIndex("dbo.Charges", new[] { "PrescriptionID" });
            DropIndex("dbo.Charges", new[] { "PaymentTypeID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PrescriptionMedicines");
            DropTable("dbo.Pictures");
            DropTable("dbo.PatientHistories");
            DropTable("dbo.Offlines");
            DropTable("dbo.PrescriptionCategories");
            DropTable("dbo.Medicines");
            DropTable("dbo.PatientTypes");
            DropTable("dbo.Patients");
            DropTable("dbo.Instructions");
            DropTable("dbo.Doctors");
            DropTable("dbo.Prescriptions");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.Charges");
        }
    }
}
