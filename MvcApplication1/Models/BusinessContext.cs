using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class BusinessContext : DbContext
    {
        public BusinessContext()
        : base("DefaultConnection")
        {

        }
        public DbSet<Business> Business { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserProfile> Users { get; set; }
    }
}