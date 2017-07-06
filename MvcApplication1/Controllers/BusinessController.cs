using MvcApplication1.Models;
using MvcApplication1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BusinessController : Controller
    {
        private BusinessService Services;

        public BusinessController()
        {
            Services = new BusinessService();
        }

        //
        // GET: /Business/
        public ActionResult Index()
        {
            var Model = Services.GetAll();
        //    var payment = Model[0].Payments[0];
            return View(Model);
        }

        //
        // GET: /Business/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Business/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Business/Create

        [HttpPost]
        public ActionResult Create(Business Model)
        {
    //        try
    //        {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    Services.create(Model);
                    return RedirectToAction("Index");
                }
    //        }
   //         catch
   //         {
                // LOG
                
   //         }
            return View();
        }

        //
        // GET: /Business/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Business/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Business Model)
        {
            try
            {
                // TODO: Add update logic here
                if(ModelState.IsValid)
                {
                    Services.edit(Model, id);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Business/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Business/Delete/5

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
