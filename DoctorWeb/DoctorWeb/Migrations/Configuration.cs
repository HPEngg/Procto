namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DoctorWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DoctorWeb.Models.ApplicationDbContext context)
        {
            context.PrescriptionCategories.AddOrUpdate(
                    new Models.PrescriptionCategory() { Name = "P1" },
                    new Models.PrescriptionCategory() { Name = "P2" });

            context.PatientTypes.AddOrUpdate(
                new Models.PatientType() { PatientTypeName = "Regular" },
                new Models.PatientType() { PatientTypeName = "Emergency" }
                );

            context.PaymentTypes.AddOrUpdate(
                new Models.PaymentType() { PaymentTypeName = "N", Rupees = 200 },
                new Models.PaymentType() { PaymentTypeName = "O", Rupees = 150 },
                new Models.PaymentType() { PaymentTypeName = "P", Rupees = 100 },
                new Models.PaymentType() { PaymentTypeName = "D", Rupees = 90 },
                new Models.PaymentType() { PaymentTypeName = "K", Rupees = 60 },
                new Models.PaymentType() { PaymentTypeName = "S", Rupees = 110 }
                );

            context.Instructions.AddOrUpdate(
                new Models.Instruction() { Name = "Normal", Description = "Take medicine regularly" },
                new Models.Instruction() { Name = "Critical", Description = "Go to hell" }
                );

            context.OINTTypes.AddOrUpdate(
                new Models.OINTType() { Name = "Syrup" },
                new Models.OINTType() { Name = "Tablet" },
                new Models.OINTType() { Name = "Capsule" }
                );

            context.Dosages.AddOrUpdate(
                new Models.Dosage() { Name = "Before food" },
                new Models.Dosage() { Name = "After food" }
                );

            context.Dozes.AddOrUpdate(
                new Models.Doz() { Name = "1" },
                new Models.Doz() { Name = "2" },
                new Models.Doz() { Name = "3" },
                new Models.Doz() { Name = "1/2" }
                );

            context.Doctors.AddOrUpdate(
                new Models.Doctor() { Name = "Dr.Andy", Address = "Kalanala, Bhavnagar", Email = "andy@dr.com", Contact = "1234567890", DoctorType = "M.S." },
                new Models.Doctor() { Name = "Dr.Trivedi", Address = "Kamatibaug, Baroda", Email = "trivedi@dr.com", Contact = "1010101010", DoctorType = "Physician" },
                new Models.Doctor() { Name = "Dr.Shah", Address = "Vastrapur, Ahmedabad", Email = "shah@dr.com", Contact = "2020202020", DoctorType = "Surgen" }
                );

        }
    }
}
