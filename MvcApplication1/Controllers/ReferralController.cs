using MvcApplication1.Filters;
using MvcApplication1.Models;
using MvcApplication1.Models.ViewModels;
using MvcApplication1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [Authorize]
    [SubscriptionFilter]
    public class ReferralController : Controller
    {
        private DoctorService DoctorRepository;
        private ReferralService ReferralServices;
        public ReferralController()
        {
            DoctorRepository = new DoctorService();
            ReferralServices = new ReferralService();
        }
        //
        // GET: /Referral/

        public ActionResult Index()
        {
            List<Referral> Referrals;
            Referrals = ReferralServices.GetAll();
           return View(Referrals);
        }

        //
        // GET: /Referral/Details/5

        public ActionResult Details(int id)
        {
            Referral Model = ReferralServices.FindById(id);
            return View(Model);
        }

        //
        // GET: /Referral/Create

        public ActionResult Create()
        {
            List<ReferralCreationVM> data = ReferralServices.GetData();
            
            return View(data);
        }

        //
        // POST: /Referral/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            
                int DoctorId = Convert.ToInt32(collection["Doctor"]);
                // TODO: Add insert logic here
                ReferralServices.Create(DoctorId, collection);

                return RedirectToAction("Index");

        }

        //
        // GET: /Referral/Edit/5

        public ActionResult Edit(int id)
        {
            Referral Target = ReferralServices.FindById(id);
            ReferralEditVM data = ReferralServices.GetDataForEdit(Target);
            
            return View(data);
        }

        //
        // POST: /Referral/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

                // TODO: Add update logic here
                int DoctorId = Convert.ToInt32(collection["Doctor"]);
                Referral Target = ReferralServices.FindById(id);
                ReferralServices.Update(DoctorId, collection, id);
                return RedirectToAction("Index");
        }

        //
        // GET: /Referral/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                ReferralServices.DeleteReferral(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Referral/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                ReferralServices.DeleteReferral(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
