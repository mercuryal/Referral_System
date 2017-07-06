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
    public class DoctorController : Controller
    {
        private DoctorService Service;
        private ReasonService ReasonRepository;
        private PaymentService PaymentRepository;
        //
        // GET: /Doctor/
        public DoctorController()
        {
            Service = new DoctorService();
            ReasonRepository = new ReasonService();
            PaymentRepository = new PaymentService();
        }

        public ActionResult Index()
        {
            List<Doctor> Doctors;
            Doctors = Service.GetAll();
            return View(Doctors);
        }

        //
        // GET: /Doctor/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateReferral(int Id)
        {
            Doctor ToDoctor;
            ToDoctor = Service.FindById(Id);
            List<DoctorReferralVM> MyEnumerable = new List<DoctorReferralVM>();
            DoctorReferralVM ReferralData;
            ReferralData = new DoctorReferralVM();
            ReferralData.DoctorModel = ToDoctor;
            ReferralData.ReasonModel = ReasonRepository.GetAll();
            MyEnumerable.Add(ReferralData);
            return View(MyEnumerable);
        }

        [HttpPost]
        public ActionResult CreateReferral(int Id, FormCollection collection)
        {
            try
            {
                Service.CreateReferral(Id, collection);
                return RedirectToAction("Edit", "Doctor", new { id = Id });
            }
            catch
            {
                Doctor ToDoctor;
                ToDoctor = Service.FindById(Id);
                List<DoctorReferralVM> MyEnumerable = new List<DoctorReferralVM>();
                DoctorReferralVM ReferralData;
                ReferralData = new DoctorReferralVM();
                ReferralData.DoctorModel = ToDoctor;
                ReferralData.ReasonModel = ReasonRepository.GetAll();
                MyEnumerable.Add(ReferralData);
                return View(MyEnumerable);
            } 
        }

         //
        // GET: /Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Doctor/Create

        [HttpPost]
        public ActionResult Create(Doctor Model)
        {
            try
            {
                // TODO: Add insert logic here
                Service.Create(Model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //POST: DOCTOR/FIND
        [HttpPost]
        public ActionResult Find(FormCollection Collection)
        {
            string Keyword = Collection["Keyword"];
            List<Doctor> Result;
            try
            {
                // TODO: Add insert logic here
                Result = Service.Find(Keyword);
                if(Result == null)
                    return View("NotFound");
                if(Result.Count == 1)
                    return RedirectToAction("Edit", "Doctor", new { id = Result[0].Id });
                if (Result.Count > 1)
                    return View("FindResult", Result);
                    //return RedirectToAction("DoctorsFound", "Doctor", new { Models = Result});

                return View("NotFound");
            }
            catch
            {
                return View("NotFound");
            }
        }

        public ActionResult DoctorsFound(Doctor Models)
        {
            return View("Index", Models);
        }

        //
        // GET: /Doctor/Edit/5

        public ActionResult Edit(int id)
        {
            Doctor doctor;
            doctor = Service.FindById(id);
            return View(doctor);
        }

        //
        // POST: /Doctor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Doctor edited)
        {
            try
            {
                // TODO: Add update logic here
                Service.Edit(edited, id);
                return RedirectToAction("Edit", "Doctor", new { Doctorid = id });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Doctor/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Doctor/Delete/5

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
