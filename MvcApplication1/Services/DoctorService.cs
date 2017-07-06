using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebMatrix.WebData;

namespace MvcApplication1.Services
{
    public class DoctorService
    {
        PatientService PatientRepository;
        ReasonService ReasonRepository;
        public DoctorService()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            PatientRepository = new PatientService();
            ReasonRepository = new ReasonService();
        }
        public void Create(Doctor DoctorData)
        {
            Doctor NewDoctor;
            using(var db = new DoctorContext())
            {
                NewDoctor = db.Doctors.Create();
                NewDoctor.Name = DoctorData.Name;
                NewDoctor.Last_name = DoctorData.Last_name;
                NewDoctor.Middle_initial = DoctorData.Middle_initial;
                NewDoctor.NAGP = DoctorData.NAGP;
                NewDoctor.NPI = DoctorData.NPI;
                NewDoctor.DEA = DoctorData.DEA;
                NewDoctor.Clinic_name = DoctorData.Clinic_name;
                NewDoctor.Office_phone = DoctorData.Office_phone;
                NewDoctor.Fax = DoctorData.Fax;
                NewDoctor.Active = true;
                NewDoctor.Email = DoctorData.Email;
                NewDoctor.Address = DoctorData.Address;
                NewDoctor.City = DoctorData.City;
                NewDoctor.State = DoctorData.State;
                NewDoctor.Notes = DoctorData.Notes;
                
                using(var db2 = new UsersContext())
                {
                    UserProfile CurrentUser = db2.UserProfiles.Include("Doctors").First(x => x.UserId.Equals(WebSecurity.CurrentUserId));
                    NewDoctor.UserProfileId = CurrentUser.UserId;
                    CurrentUser.Doctors.Add(NewDoctor);

                    db2.SaveChanges();
                }
            }
        }



        public void CreateReferral(int DoctorId, FormCollection Collection)
        {
            Referral newReferral;
            Patient Patient;
            string patientEmail = Collection["PatientEmail"];
            List<Patient> patients;
            Doctor TargetDoctor;
            int reasonId;
            using(var db = new ReferralContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                reasonId = Convert.ToInt32(Collection["Reason"]);
                Patient = new Patient();
                newReferral = db.Referrals.Create();

                newReferral.Date = DateTime.Parse(Collection["Date"]);
                newReferral.Note = Collection["Note"];
                newReferral.City = Collection["City"];
                newReferral.State = Collection["State"];
                //Adding patient
                patients = db.Patient.Where(x => x.Email.Equals(patientEmail) && x.UserProfileId.Equals(WebSecurity.CurrentUserId)).ToList();
                if(patients.Count < 1)
                {
                    Patient = new Patient();
                    Patient.First_name = Collection["Name"];
                    Patient.Middle_initial = Collection["Middle_initial"];
                    Patient.Last_name = Collection["Last_name"];
                    Patient.Email = Collection["PatientEmail"];
                    Patient.UserProfileId = WebSecurity.CurrentUserId;
                    newReferral.Patient = Patient;
                }
                else
                {
                    newReferral.Patient = patients[0];
                    db.Entry(newReferral.Patient).State = EntityState.Unchanged;
                }

                //Adding reason
                newReferral.ReasonId = reasonId;

                TargetDoctor = db.Doctor.Include("Referrals").First(x => x.Id.Equals(DoctorId));
                TargetDoctor.Referrals.Add(newReferral);
                db.Referrals.Add(newReferral);
                db.SaveChanges();
                //save db changes
                //AddReferral(DoctorId, newReferral);
            }
        }

        //Store a referral into database
        public void AddReferral(int DoctorId, Referral Referral)
        {
            Doctor TargetDoctor;
            using(var db2 = new DoctorContext())
            {
                TargetDoctor = db2.Doctors.Include("Referrals").First(x => x.Id.Equals(DoctorId));
                db2.Entry(TargetDoctor).State = EntityState.Modified;
                TargetDoctor.Referrals.Add(Referral);
                db2.SaveChanges();
            }
        }

        public void QuitReferral(int DoctorId, Referral Referral)
        {
            Doctor TargetDoctor;
            using (var db2 = new DoctorContext())
            {
                TargetDoctor = db2.Doctors.Include("Referrals").First(x => x.Id.Equals(DoctorId));
                TargetDoctor.Referrals.Remove(Referral);
                db2.Entry(TargetDoctor).State = EntityState.Modified;
                db2.SaveChanges();
            }
        }

        public List<Doctor> GetAll()
        {
            Doctor[] DocArray;
            using(var db = new DoctorContext())
            {
                IEnumerable<Doctor> Doctors = db.Doctors.Include("Referrals").Where(x => x.UserProfileId.Equals(WebSecurity.CurrentUserId));
                DocArray = Doctors.ToArray();
                return Doctors.ToList();
            }
        }

        public List<Doctor> Find(string Keyword)
        {
            using(var db = new DoctorContext())
            {
                IEnumerable<Doctor> Doctors = db.Doctors.Include("Referrals").Where(x => x.UserProfileId.Equals(WebSecurity.CurrentUserId) && 
                                                                                     (x.Name.Contains(Keyword) || x.Last_name.Contains(Keyword) || x.Email.Contains(Keyword) || x.City.Contains(Keyword) || x.State.Contains(Keyword) || x.Clinic_name.Contains(Keyword) ));
                return Doctors.ToList();
            }
        }

        public Doctor FindById(int id)
        {
            Doctor doctor;
            using(var db = new DoctorContext())
            {
                doctor = db.Doctors.Include(x => x.Referrals.Select(l1 => l1.Reason)).Include(z => z.Referrals.Select(l2 => l2.Patient)).First(x => x.UserProfileId.Equals(WebSecurity.CurrentUserId) && x.Id.Equals(id));
              //  using(var db2 = new ReferralContext())
              //  {
              //      Refs = db2.Referrals.Include("Reason").Where(x => x.Doctor_Id.Equals(id)).ToList();
              //  }
              //  doctor.Referrals = Refs;
                return doctor;
            }
        }

        public void Edit(Doctor newData, int id)
        {
            Doctor Modified;
            using(var db = new DoctorContext())
            {
                Modified = db.Doctors.Find(id);
                Modified.Active = newData.Active;
                Modified.Name = newData.Name;
                Modified.Middle_initial = newData.Middle_initial;
                Modified.Last_name = newData.Last_name;
                Modified.Clinic_name = newData.Clinic_name;
                Modified.Address = newData.Address;
                Modified.Email = newData.Email;
                Modified.Fax = newData.Fax;
                Modified.Office_phone = newData.Office_phone;
                Modified.DEA = newData.DEA;
                Modified.NAGP = newData.NAGP;
                Modified.NPI = newData.NPI;
                Modified.Notes = newData.Notes;
                db.SaveChanges();

            }
        }
    }
}