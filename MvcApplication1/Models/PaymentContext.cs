using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class PaymentContext : DbContext
    {
        public PaymentContext() : base("DefaultConnection")
        {
            
        }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
    }
}