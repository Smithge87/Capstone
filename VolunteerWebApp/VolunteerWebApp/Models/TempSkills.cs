using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class TempSkills
    {
        [Key]
        public int ID { get; set; }
        public Opportunity Opportunity { get; set; }
        public int OpportunityId { get; set; }
        public string OrganizationId { get; set; }
        public string Category { get; set; }
        public string SkillLevel { get; set; }
        public string Amount { get; set; }
        public string SkillImgSrc { get; set; }
    }
}