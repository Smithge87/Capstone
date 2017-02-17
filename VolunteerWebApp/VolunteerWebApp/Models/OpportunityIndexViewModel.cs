using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class OpportunityIndexViewModel
    {
        private ApplicationDbContext _context;

        public OpportunityIndexViewModel()
        {
            _context = new ApplicationDbContext();
        }

        public Opportunity Opportunity { get; set; }
        public IEnumerable<SkillsNeeded> SkillsNeededList { get; set; }
        public IEnumerable<Interest> InterestedUsers { get; set; }
        public string InterestSet { get; set; }
        public int oppId { get; set; }

    }
}