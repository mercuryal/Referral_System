using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class ReasonContext : DbContext
    {
        public ReasonContext() : base("DefaultConnection")
        {

        }
        public DbSet<Reason> Reason { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
    }
}