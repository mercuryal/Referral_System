using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MvcApplication1.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Last_name{get; set;}
        public string Middle_initial {get; set;}
        public string Clinic_name {get; set;}
        public string DEA {get; set;}
        public string NAGP {get; set;}
        public string NPI {get; set;}
        public string Office_phone {get; set;}
        public string Fax {get; set;}
        public string Email {get; set;}
        public string Address { get; set; }
        public string Notes { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Active {get; set;}
        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        [ScriptIgnore]
        public List<Referral> Referrals { get; set; }
    }
}