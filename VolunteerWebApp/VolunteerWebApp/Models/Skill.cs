using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class Skill
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public string AnimalSkill { get; set; }
        public string DisasterSkill { get; set; }
        public string EducationSkill { get; set; }
        public string EnviornmentSkill { get; set; }
        public string HealthSkill { get; set; }
        public string HumanServicesSkill { get; set; }

    }
}