using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DoctorWeb.Models.Tools;
using System.Data.Entity.Infrastructure.Interception;

namespace DoctorWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {
            DbInterception.Add(new OfflineInterceptor());
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Medicine>()
            //        .HasRequired(m => m.Morning)
            //        .WithMany(t => t.MorningMedicines)
            //        .HasForeignKey(m => m.MorningDozID)
            //        .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Medicine>()
            //        .HasRequired(m => m.Noon)
            //        .WithMany(t => t.NoonMedicines)
            //        .HasForeignKey(m => m.NoonDozID)
            //        .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Medicine>()
            //        .HasRequired(m => m.Night)
            //        .WithMany(t => t.NightMedicines)
            //        .HasForeignKey(m => m.NightDozID)
            //        .WillCascadeOnDelete(false);

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientType> PatientTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public DbSet<Offline> OfflineRecords { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<PrescriptionCategory> PrescriptionCategories { get; set; }
        public DbSet<OINTType> OINTTypes { get; set; }
        public DbSet<Dosage> Dosages { get; set; }
        public DbSet<Doz> Dozes { get; set; }


        public System.Data.Entity.DbSet<DoctorWeb.Models.PatientHistory> PatientHistories { get; set; }
    }
}