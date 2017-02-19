using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class PartialProfileVolunteerViewModel
    {
        private ApplicationDbContext _context;
        public PartialProfileVolunteerViewModel()
        {
            _context = new ApplicationDbContext();
        }
        public ApplicationUser Volunteer { get; set; }
        public Skill VolunteerSkills { get; set; }
        public Interest OpportunityInterest { get; set; }
        public Information VulunteerInfo { get; set; }
        public string AnimalImageSrc { get; set; }
        public string DisasterImageSrc { get; set; }
        public string EducationImageSrc { get; set; }
        public string EnviornmentImageSrc { get; set; }
        public string HealthImageSrc { get; set; }
        public string HumanServicesImageSrc { get; set; }
    }
}