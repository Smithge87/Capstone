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
        public string AboutAnimal { get; set; }
        public string AboutDisaster { get; set; }
        public string AboutEducation { get; set; }
        public string AboutEnviornment { get; set; }
        public string AboutHealth { get; set; }
        public string AboutHumanServices { get; set; }
        public string AboutSkills{ get; set; }
    }
}