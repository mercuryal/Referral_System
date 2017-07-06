using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace MvcApplication1.Services
{
    public class PatientService
    {
        public PatientService()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }

        public List<Patient> GetAll()
        {
            using(var db = new PatientContext())
            {
                return db.Patients.Include("Referrals").Where(x => x.UserProfileId.Equals(WebSecurity.CurrentUserId)).ToList();
            }
        }

        public void Create(Patient PatientData)
        {
            Patient NewPatient;

            using(var db = new PatientContext())
            {
                NewPatient = db.Patients.Create();
                NewPatient.First_name = PatientData.First_name;
                NewPatient.Last_name = PatientData.Last_name;
                NewPatient.Middle_initial = PatientData.Middle_initial;
                NewPatient.Phone = PatientData.Phone;
                NewPatient.Address = PatientData.Address;
                NewPatient.Email = PatientData.Email;
                NewPatient.UserProfile = db.UsersProfile.Find(WebSecurity.CurrentUserId);

                db.Patients.Add(NewPatient);
                db.SaveChanges();
            }
        }

        public List<Patient> Find(string Keyword)
        {
            using (var db = new PatientContext())
            {
                IEnumerable<Patient> Patients = db.Patients.Include("Referrals").Where(x => x.UserProfileId.Equals(WebSecurity.CurrentUserId) &&
                                                                                     (x.First_name.Contains(Keyword) || x.Last_name.Contains(Keyword) || x.Email.Contains(Keyword)));
                return Patients.ToList();
            }
        }

        public Patient FindById(int id)
        {
            Patient patient;
            using(var db = new PatientContext())
            {
                patient = db.Patients.Find(id);
                return patient;
            }
        }

        public void Edit(int id, Patient PatientData)
        {
            Patient patient;
            using(var db = new PatientContext())
            {
                patient = db.Patients.Find(id);
                patient.First_name = PatientData.First_name;
                patient.Middle_initial = PatientData.Middle_initial;
                patient.Last_name = patient.Last_name;
                patient.Email = PatientData.Email;
                patient.Phone = PatientData.Phone;
                patient.Address = PatientData.Address;

                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}