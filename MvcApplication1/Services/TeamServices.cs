using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication1.Models;

namespace MvcApplication1.Services
{
    public class TeamServices
    {
        public List<Team> getTeamsList()
        {
            using (var db = new TeamContext())
            {
                return db.Teams.ToList();
            }
        }
    }
}