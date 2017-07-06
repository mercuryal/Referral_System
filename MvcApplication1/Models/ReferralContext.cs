using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class ReferralContext : DbContext
    {
        public ReferralContext() : base("DefaultConnection") { }

        public DbSet<Referral> Referrals{get; set;}
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Reason> Reason { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here
            modelBuilder.Entity<Referral>().HasRequired<Reason>(r => r.Reason).WithMany(r => r.Referrals).WillCascadeOnDelete(false);
            modelBuilder.Entity<Referral>().HasRequired<Patient>(s => s.Patient).WithMany(s => s.Referrals).WillCascadeOnDelete(false);
            modelBuilder.Entity<Referral>().HasRequired<Doctor>(d => d.Doctor).WithMany(d => d.Referrals).WillCascadeOnDelete(false);
            //base.OnModelCreating(modelBuilder);
        }
    }
}