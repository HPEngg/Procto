namespace DoctorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExpanseFeature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpanseCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ExpanseCategory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExpanseCategories", t => t.ExpanseCategory_ID)
                .Index(t => t.ExpanseCategory_ID);
            
            CreateTable(
                "dbo.Expanses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        ExpanseCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExpanseCategories", t => t.ExpanseCategoryID)
                .Index(t => t.ExpanseCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expanses", "ExpanseCategoryID", "dbo.ExpanseCategories");
            DropForeignKey("dbo.ExpanseCategories", "ExpanseCategory_ID", "dbo.ExpanseCategories");
            DropIndex("dbo.Expanses", new[] { "ExpanseCategoryID" });
            DropIndex("dbo.ExpanseCategories", new[] { "ExpanseCategory_ID" });
            DropTable("dbo.Expanses");
            DropTable("dbo.ExpanseCategories");
        }
    }
}
