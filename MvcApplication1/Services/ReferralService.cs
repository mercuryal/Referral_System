using MvcApplication1.Models;
using MvcApplication1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;


namespace MvcApplication1.Services
{
    public class ReferralService
    {
        public DoctorService DoctorsRepository;
        public ReasonService ReasonRepository;
        //class constructor
        public ReferralService()
        {
            ReasonRepository = new ReasonService();
            DoctorsRepository = new DoctorService();
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }

        //Get a list of referrals registered by the current user
        public List<Referral> GetAll()
        {
            using(var db = new ReferralContext())
            {
                return db.Referrals.Include("Doctor").Include("Patient").Include("Reason").Where(x => x.Doctor.UserProfileId.Equals(WebSecurity.CurrentUserId)).ToList();  //OrderByDescending(item => item.Date).ToList();
            }
        }

        //Get the data needed for display referral form
        public List<ReferralCreationVM> GetData()
        {
            ReferralCreationVM element = new ReferralCreationVM();
            element.DoctorModels = DoctorsRepository.GetAll();
            element.ReasonModels = ReasonRepository.GetAll();

            List<ReferralCreationVM> data = new List<ReferralCreationVM>();
            data.Add(element);

            return data;
        }

        //Get the data needed for the edit referral view (ModelView)
        public ReferralEditVM GetDataForEdit(Referral currentData)
        {
            ReferralEditVM element = new ReferralEditVM();
            element.DoctorModels = DoctorsRepository.GetAll();
            element.ReasonModels = ReasonRepository.GetAll();
            element.CurrentDoctor = currentData.Doctor;
            element.CurrentPatient = currentData.Patient;
            element.CurrentReason = currentData.Reason;
            element.ReferralModel = currentData;

            return element;
        }

        //Create a new referral
        public void Create(int DoctorId, FormCollection Collection)
        {
            Referral referral;
            Patient patient;
            Doctor doctor;
            List<Patient> patients;
            string patientEmail = Collection["PatientEmail"];
            using(var db = new ReferralContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                referral = db.Referrals.Create();
                referral.City = Collection["City"];
                referral.State = Collection["State"];
                referral.Date = DateTime.Parse(Collection["Date"]);
                referral.Note = Collection["Notes"];
                int reasonId = Convert.ToInt32(Collection["Reason"]);
                referral.Reason = db.Reason.FirstOrDefault(x => x.Id.Equals(reasonId));
                db.Entry(referral.Reason).State = EntityState.Unchanged;

                //adding patient
                patients = db.Patient.Where(x => x.Email.Equals(patientEmail) && x.UserProfileId.Equals(WebSecurity.CurrentUserId)).ToList();
                if(patients.Count < 1)
                {
                    patient = new Patient();
                    patient.First_name = Collection["Name"];
                    patient.Middle_initial = Collection["Middle_initial"];
                    patient.Last_name = Collection["Last_name"];
                    patient.Email = Collection["PatientEmail"];
                    patient.UserProfileId = WebSecurity.CurrentUserId;
                    referral.Patient = patient;
                }
                else
                {
                    referral.Patient = patients[0];
                    db.Entry(referral.Patient).State = EntityState.Unchanged;
                }

                doctor = db.Doctor.Find(DoctorId);
                referral.Doctor = doctor;
                db.Referrals.Add(referral);
                db.SaveChanges();
             //   DoctorsRepository.AddReferral(DoctorId, referral);
            }
        }

        //Update a referral
        public void Update(int DoctorId, FormCollection Collection, int ReferralId)
        {
            Referral referral;
            using (var db = new ReferralContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                referral = db.Referrals.First(x => x.Doctor.UserProfileId.Equals(WebSecurity.CurrentUserId) && x.Id.Equals(ReferralId));
                db.Entry(referral).State = EntityState.Modified;
                referral.City = Collection["City"];
                referral.State = Collection["State"];
                referral.Date = DateTime.Parse(Collection["Date"]);
                referral.Note = Collection["Notes"];
                referral.Reason = db.Reason.Find(Convert.ToInt32(Collection["Reason"]));

                if(referral.DoctorId != DoctorId)
                {
                    referral.Doctor = db.Doctor.Where(x => x.Id.Equals(DoctorId)).First();
                    db.SaveChanges();
                }
                else
                {
                 //   db.Referrals.Attach(referral);
                    db.SaveChanges();
                }
            }
        }

        //Find a referral by Id
        public Referral FindById(int id)
        {
            using(var db = new ReferralContext())
            {
                return db.Referrals.Include("Doctor").Include("Reason").Include("Patient").First(x => x.Id.Equals(id));
            }
        }

        //Delete a referral
        public void DeleteReferral(int id)
        {
            Referral ReferralDelete;
            using(var db = new ReferralContext())
            {
                ReferralDelete = db.Referrals.First(x => x.Doctor.UserProfileId.Equals(WebSecurity.CurrentUserId) && x.Id.Equals(id));
                db.Referrals.Remove(ReferralDelete);
                db.SaveChanges();
            }
        }
    }
}