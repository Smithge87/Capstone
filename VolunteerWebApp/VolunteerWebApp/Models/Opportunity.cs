using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class Opportunity
    {
        [Key]
        public int ID { get; set; }
        public string Category { get; set; }
        public string OrganizationHostId { get; set; }
        public string OrganizationHostName { get; set; }
        public string StartDate { get; set; }
        public string Duration { get; set; }
        public string Address { get; set; }
        public string GeoLocation { get; set; }
        public string AboutOpportunity { get; set; }
    }
}