using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using WebMatrix.WebData;
using System.Web.Mvc;

namespace MvcApplication1.Services
{
    public class BusinessService
    {
        public BusinessService()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }

        public void create(Business business)
        {
            using (var db = new BusinessContext())
            {
                UserProfile CurrentUser;
                Business NewBusiness = db.Business.Create();
                NewBusiness.Addres = business.Addres;
                NewBusiness.Contact_name = business.Contact_name;
                NewBusiness.Email = business.Email;
                NewBusiness.Name = business.Name;
                NewBusiness.Phone = business.Phone;
                
                using(var db2 = new UsersContext())
                {
                    CurrentUser = db2.UserProfiles.Include("Business").First(x => x.UserId.Equals(WebSecurity.CurrentUserId));
                    NewBusiness.UserProfile = CurrentUser;
                    NewBusiness.UserProfileId = CurrentUser.UserId;
                    CurrentUser.Business.Add(NewBusiness);
                    db2.SaveChanges();
                }

                db.Business.Add(NewBusiness);
                db.SaveChanges();
            }
        }

        public void create(FormCollection collection)
        {
            using(var db = new BusinessContext())
            {
                UserProfile CurrentUser;
                Business NewBusiness = db.Business.Create();
                NewBusiness.Addres = collection["BusinessAddress"];
                NewBusiness.Name = collection["BusinessName"];
                NewBusiness.Phone = collection["Phone"];
                NewBusiness.Email = collection["Email"];
                NewBusiness.Contact_name = collection["Contact_name"];

                CurrentUser = db.Users.First(x => x.UserId.Equals(WebSecurity.CurrentUserId));
                NewBusiness.UserProfile = CurrentUser;

                db.Business.Add(NewBusiness);
                db.SaveChanges();
            }
        }

        public List<Business> GetAll()
        {
            using (var db = new BusinessContext())
            {
                if(!WebSecurity.Initialized)
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                int MyId = WebSecurity.CurrentUserId;
                IEnumerable<Business> MyBusiness = db.Business.Where(x => x.UserProfile.UserId.Equals(MyId));
           
                return MyBusiness.ToList();
            }
        }

        public void edit(Business model, int id)
        {
            using (var db = new BusinessContext())
            {
                Business UpdatedBusiness = db.Business.Find(id);

                UpdatedBusiness.Addres = model.Addres;
                UpdatedBusiness.Contact_name = model.Contact_name;
                UpdatedBusiness.Email = model.Email;
                UpdatedBusiness.Name = model.Name;
                UpdatedBusiness.Phone = model.Phone;

                db.SaveChanges();
            }
        }
    }
}