using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Referral
    {
        public int Id {get; set;}
        public string City { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
        public int PatientId{get; set;}
        public virtual Patient Patient { get; set; } 
        public string Note { get; set; }
        public int ReasonId{get; set;}
        public virtual Reason Reason { get; set; } 
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}