namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Referral : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Referrals", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Referrals", "Reason_Id", "dbo.Reasons");
            DropIndex("dbo.Referrals", new[] { "Patient_Id" });
            DropIndex("dbo.Referrals", new[] { "Reason_Id" });
            RenameColumn(table: "dbo.Referrals", name: "Doctor_Id", newName: "DoctorId");
            RenameColumn(table: "dbo.Referrals", name: "Patient_Id", newName: "PatientId");
            RenameColumn(table: "dbo.Referrals", name: "Reason_Id", newName: "ReasonId");
        //    RenameIndex(table: "dbo.Referrals", name: "IX_Doctor_Id", newName: "IX_DoctorId");
            AlterColumn("dbo.Referrals", "PatientId", c => c.Int(nullable: false));
            AlterColumn("dbo.Referrals", "ReasonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Referrals", "PatientId");
            CreateIndex("dbo.Referrals", "ReasonId");
            AddForeignKey("dbo.Referrals", "PatientId", "dbo.Patients", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Referrals", "ReasonId", "dbo.Reasons", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Referrals", "ReasonId", "dbo.Reasons");
            DropForeignKey("dbo.Referrals", "PatientId", "dbo.Patients");
            DropIndex("dbo.Referrals", new[] { "ReasonId" });
            DropIndex("dbo.Referrals", new[] { "PatientId" });
            AlterColumn("dbo.Referrals", "ReasonId", c => c.Int());
            AlterColumn("dbo.Referrals", "PatientId", c => c.Int());
            RenameIndex(table: "dbo.Referrals", name: "IX_DoctorId", newName: "IX_Doctor_Id");
            RenameColumn(table: "dbo.Referrals", name: "ReasonId", newName: "Reason_Id");
            RenameColumn(table: "dbo.Referrals", name: "PatientId", newName: "Patient_Id");
            RenameColumn(table: "dbo.Referrals", name: "DoctorId", newName: "Doctor_Id");
            CreateIndex("dbo.Referrals", "Reason_Id");
            CreateIndex("dbo.Referrals", "Patient_Id");
            AddForeignKey("dbo.Referrals", "Reason_Id", "dbo.Reasons", "Id");
            AddForeignKey("dbo.Referrals", "Patient_Id", "dbo.Patients", "Id");
        }
    }
}
