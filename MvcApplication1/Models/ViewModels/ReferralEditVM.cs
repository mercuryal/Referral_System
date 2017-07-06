using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models.ViewModels
{
    public class ReferralEditVM
    {
        public List<Doctor> DoctorModels;
        public Doctor CurrentDoctor;
        public Referral ReferralModel;
        public Patient CurrentPatient;
        public List<Reason> ReasonModels;
        public Reason CurrentReason;
    }
}