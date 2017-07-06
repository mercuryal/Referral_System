using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class DoctorContext : DbContext
    {
        public DoctorContext():base("DefaultConnection"){}

        public DbSet<Doctor> Doctors { get; set; }
    }
}