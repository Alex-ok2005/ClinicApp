namespace ClinicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitClinicData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(maxLength: 50),
                        Patronymic = c.String(maxLength: 50),
                        Sex = c.Boolean(),
                        BirthDate = c.DateTime(),
                        Address = c.String(),
                        Phone = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InitialVisit = c.Boolean(nullable: false),
                        DateVisit = c.DateTime(nullable: false),
                        Diagnosis = c.String(),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "PatientId", "dbo.Patients");
            DropIndex("dbo.Visits", new[] { "PatientId" });
            DropTable("dbo.Visits");
            DropTable("dbo.Patients");
        }
    }
}
