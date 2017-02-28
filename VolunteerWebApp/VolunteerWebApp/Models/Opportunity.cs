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
        public string StartTime { get; set; }
        public string EndTime { get; set;}
        public string StartDay { get; set; }
        public string StartMonth { get; set; }
        public string StartYear { get; set; }
        public string Duration { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string AboutOpportunity { get; set; }
        public string AboutShort { get; set; }
        public string LogoSrc { get; set; }
    }
}