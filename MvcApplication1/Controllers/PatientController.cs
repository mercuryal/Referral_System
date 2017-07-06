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
    public class PatientController : Controller
    {
        private PatientService Service;
        private PaymentService PaymentRepository;
        public PatientController()
        {
            Service = new PatientService();
            PaymentRepository = new PaymentService();

        }
        //
        // GET: /Patient/

        public ActionResult Index()
        {
            List<Patient> PatientsData;
            PatientsData = Service.GetAll();
            return View(PatientsData);
        }

        //
        // GET: /Patient/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Patient/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Patient/Create

        [HttpPost]
        public ActionResult Create(Patient Model)
        {
            try
            {
                // TODO: Add insert logic here
                Service.Create(Model);
                return RedirectToAction("Index", "Patient");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Find(FormCollection Collection)
        {
            string Keyword = Collection["Keyword"];
            List<Patient> Result;
            try
            {
                Result = Service.Find(Keyword);
                if (Result.Count > 0)
                    return View("FindResult", Result);
                else
                    return View("NotFound");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Patient/Edit/5

        public ActionResult Edit(int id)
        {
            Patient Model = Service.FindById(id);
            return View(Model);
        }

        //
        // POST: /Patient/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Patient PatientModel)
        {
            try
            {
                // TODO: Add update logic here
                Service.Edit(id, PatientModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Patient/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Patient/Delete/5

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

        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }
    }
}
