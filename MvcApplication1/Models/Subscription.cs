using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Subscription
    {
        [Key]
        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
        public string Status { get; set; }
        public string BillingAddress { get; set; }
        public string CreditCard { get; set; }
        public string Zip { get; set;}
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CVVCode { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public List<Payment> Payments {get; set;}
    }
}