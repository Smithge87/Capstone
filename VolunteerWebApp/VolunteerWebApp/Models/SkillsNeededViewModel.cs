using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class SkillsNeededViewModel
    {
        private ApplicationDbContext _context;

        public SkillsNeededViewModel()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<TempSkills> TempSkillsList { get; set; }
        public IEnumerable<Categories> CategoryList { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public string Amount { get; set; }

    }
}