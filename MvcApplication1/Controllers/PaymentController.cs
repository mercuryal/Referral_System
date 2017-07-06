using MvcApplication1.Models;
using MvcApplication1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MvcApplication1.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private PaymentService Services;
        private BusinessService BusinessRepository;
        
        public PaymentController()
        {
            Services = new PaymentService();
            BusinessRepository = new BusinessService();
        }
        //
        // GET: /Payment/

        public ActionResult Index()
        {
            List<Subscription> Model = Services.GetAll();
            return View(Model);
        }

        //
        // GET: /Payment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Payment/Create
        public ActionResult Create()
        {
            return View("CreateSub");
        }

        //
        // GET: /Payment/ActiveSub
        public ActionResult ActiveSub()
        {
            return View();
        }

        public ActionResult NoSub()
        {
            return View();
        }

        public ActionResult SubscriptionExpired()
        {
            return View("Error");
        }

        //
        // POST: /Payment/ActiveSub
        [HttpPost]
        public ActionResult ActiveSub(FormCollection collection)
        {
            Services.CreateSub();
            return RedirectToAction("Index");
        }

        //
        // POST: /Payment/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
       //     try
       //     {
                // TODO: Add insert logic here
                Services.CreateSub(collection);
                BusinessRepository.create(collection);
                return RedirectToAction("Index");
       /*     }
            catch
            {
                return View("CreateSub");
            } */
        }

        public ActionResult Confirm()
        {
            List<Subscription> Model = Services.GetAll();
            return View(Model);
        }

        //
        // GET: /Payment/Edit/5

        public ActionResult Edit(int id)
        {
            Subscription sub = Services.GetSub(id);
            return View(sub);
        }

        //
        // POST: /Payment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Subscription sub)
        {
            try
            {
                // TODO: Add update logic here
                Services.UpdatePaymentInfo(sub);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(sub);
            }
        }

        //
        // GET: /Payment/Suspend/5

        public ActionResult Suspend(int id)
        {
            Subscription subscription = Services.GetSub(id);
            Services.SuspendSubscription(subscription);
            return RedirectToAction("Index");
        }

        public ActionResult ReactiveFromOverdue()
        {
            Services.PayAllOverdues();
            return RedirectToAction("Index");
        }

        public ActionResult ConfirmReactivation(int id)
        {
            Services.ReactiveSubscription(id);
            return RedirectToAction("Index");
        }

        //
        // POST: /Payment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
