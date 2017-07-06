namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscription : DbMigration
    {
        public override void Up()
        {
           // RenameColumn(table: "dbo.Referrals", name: "DoctorId", newName: "Doctor_Id");
           // RenameIndex(table: "dbo.Referrals", name: "IX_DoctorId", newName: "IX_Doctor_Id");
            AddColumn("dbo.Referrals", "City", c => c.String());
            AddColumn("dbo.Referrals", "Reason_Id", c => c.Int());
            AddColumn("dbo.Subscriptions", "BillingAddress", c => c.String());
            AddColumn("dbo.Subscriptions", "Zip", c => c.String());
            AddColumn("dbo.Subscriptions", "CarNumber", c => c.String());
            AddColumn("dbo.Subscriptions", "ExpirationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Subscriptions", "CVVCode", c => c.String());
            CreateIndex("dbo.Referrals", "Reason_Id");
            AddForeignKey("dbo.Referrals", "Reason_Id", "dbo.Reasons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Referrals", "Reason_Id", "dbo.Reasons");
            DropIndex("dbo.Referrals", new[] { "Reason_Id" });
            DropColumn("dbo.Subscriptions", "CVVCode");
            DropColumn("dbo.Subscriptions", "ExpirationDate");
            DropColumn("dbo.Subscriptions", "CarNumber");
            DropColumn("dbo.Subscriptions", "Zip");
            DropColumn("dbo.Subscriptions", "BillingAddress");
            DropColumn("dbo.Referrals", "Reason_Id");
            DropColumn("dbo.Referrals", "City");
            RenameIndex(table: "dbo.Referrals", name: "IX_Doctor_Id", newName: "IX_DoctorId");
            RenameColumn(table: "dbo.Referrals", name: "Doctor_Id", newName: "DoctorId");
        }
    }
}
