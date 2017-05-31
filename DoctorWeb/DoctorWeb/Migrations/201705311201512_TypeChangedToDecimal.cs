namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeChangedToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prescriptions", "M", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prescriptions", "Less", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prescriptions", "Other", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prescriptions", "Other", c => c.String());
            AlterColumn("dbo.Prescriptions", "Less", c => c.String());
            AlterColumn("dbo.Prescriptions", "M", c => c.String());
        }
    }
}
