using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string First_name { get; set; }
        public string Middle_initial { get; set; }
        public string Last_name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<Referral> Referrals { get; set; }

    }
}