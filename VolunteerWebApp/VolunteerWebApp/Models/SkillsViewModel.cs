using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class SkillsViewModel
    {
        private ApplicationDbContext _context;

        public SkillsViewModel()
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
    }
}
