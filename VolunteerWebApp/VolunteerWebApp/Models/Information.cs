using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class Information
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public int StateId { get; set; }
        public string Zipcode { get; set; }
        public string AboutInfo { get; set; }
    }
}