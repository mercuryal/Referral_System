using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class PatientContext : DbContext
    {
        public PatientContext() : base("DefaultConnection") { }

        public DbSet<Patient> Patients{get; set;}
        public DbSet<UserProfile> UsersProfile { get; set; }
    }
}