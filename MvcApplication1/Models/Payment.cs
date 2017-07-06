using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date{get; set;}
        public float Amount{get; set;}
        public string Status { get; set;}
        [ForeignKey("Subscription")]
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}