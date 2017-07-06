using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models.ViewModels
{
    public class DoctorReferralVM
    {
        public Doctor DoctorModel;
        public Referral ReferralModel;
        public Patient PatientModel;
        public List<Reason> ReasonModel;
    }
}