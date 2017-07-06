using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class TeamContext : DbContext
    {
        public TeamContext()
            :base("DefaultConnection")
        {

        }

        public DbSet<Team> Teams { get; set; }

    }
}