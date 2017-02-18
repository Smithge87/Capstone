using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class VolunteerSkillsViewModel
    {
        private ApplicationDbContext _context;

        public VolunteerSkillsViewModel()
        {
            _context = new ApplicationDbContext();
        }

        [Required]
        public string Animal { get; set; }

        [Required]
        public string Disaster { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Enviornment { get; set; }

        [Required]
        public string Health { get; set; }

        [Required]
        public string HumanServices { get; set; }

        [Required]
        public string AboutSkills { get; set; }
        public string AboutAnimal { get; set; }
        public string AboutDisaster { get; set; }
        public string AboutEducation { get; set; }
        public string AboutEnviornment { get; set; }
        public string AboutHealth { get; set; }
        public string AboutHumanServices { get; set; }
    }
}
