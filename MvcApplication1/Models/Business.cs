using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Business
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }
        public string Contact_name { get; set; }
        public string Email { get; set; }
        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}