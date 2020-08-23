namespace ClinicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorTableMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "Image");
        }
    }
}
