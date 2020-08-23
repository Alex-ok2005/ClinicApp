namespace ClinicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSexFieldMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Gender", c => c.Boolean());
            DropColumn("dbo.Patients", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Sex", c => c.Boolean());
            DropColumn("dbo.Patients", "Gender");
        }
    }
}
