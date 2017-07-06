using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class SubscriptionContext : DbContext
    {
            public SubscriptionContext()
                : base("DefaultConnection")
            {

            }
            public DbSet<Subscription> Subscription { get; set; }
        }
}