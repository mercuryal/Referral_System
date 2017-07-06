using MvcApplication1.Filters;
using MvcApplication1.Models;
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
    public class ReasonController : Controller
    {
        //
        // GET: /Reason/
        ReasonService Services;

        public ReasonController()
        {
            Services = new ReasonService();
        }
        public ActionResult Index()
        {
            List<Reason> Reasons = Services.GetAll();
            return View(Reasons);
        }

        //
        // GET: /Reason/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Reason/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reason/Create

        [HttpPost]
        public ActionResult Create(Reason Model, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Services.Create(Model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reason/Edit/5

        public ActionResult Edit(int id)
        {
            Reason Model = Services.Find(id);
            return View(Model);
        }

        //
        // POST: /Reason/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Reason Model)
        {
            try
            {
                // TODO: Add update logic here
                Services.Edit(Model, id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reason/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reason/Delete/5

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
