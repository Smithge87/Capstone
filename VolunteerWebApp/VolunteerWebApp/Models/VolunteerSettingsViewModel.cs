using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class VolunteerSettingsViewModel
    {
        private ApplicationDbContext _context;

        public VolunteerSettingsViewModel()
        {
            _context = new ApplicationDbContext();
        }

        [Required]
        public string CanContact { get; set; }
        [Required]
        public string CanSee { get; set; }
        [Required]
        public string CanRefer { get; set; }
    }
}