using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class Interest
    {
        [Key]
        public int ID { get; set; }
        public int OpportunityId { get; set; }
        public string VolunteerId { get; set; }
        public string VolunteerUserName { get; set; }
        public string InterestLevel { get; set; }
        public bool CanShow { get; set; }
        public bool CanContact { get; set; }
    }
}