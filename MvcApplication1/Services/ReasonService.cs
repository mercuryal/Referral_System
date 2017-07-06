using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace MvcApplication1.Services
{
    public class ReasonService
    {
        public ReasonService()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }

        //Get the list of all reason registered by the current user
        public List<Reason> GetAll()
        {
            using(var db = new ReasonContext())
            {
                return db.Reason.Include("Referrals").Where(x => x.UserProfileId.Equals(WebSecurity.CurrentUserId)).ToList();
            }
        }

        //create a new reason
        public void Create(Reason Model)
        {
            using(var db = new ReasonContext())
            {
                Reason reason = db.Reason.Create();
                reason.Name = Model.Name;
                reason.Description = Model.Description;

                reason.UserProfile = db.UserProfile.Find(WebSecurity.CurrentUserId);

                db.Reason.Add(reason);
                db.SaveChanges();
            }
        }

        //Find a reason
        public Reason Find(int id)
        {
            using(var db = new ReasonContext())
            {
                return db.Reason.Find(id);
            }
        }

        //Edit a reason content
        public void Edit(Reason Model, int id)
        {
            Reason editedReason;
            using(var db = new ReasonContext())
            {
                editedReason = db.Reason.Find(id);
                db.Entry(editedReason).State = EntityState.Modified;
                editedReason.Name = Model.Name;
                editedReason.Description = Model.Description;
                db.SaveChanges();
            }
        }
    }
}