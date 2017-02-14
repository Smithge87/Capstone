using System;
using System.Collections.Generic;
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

    }
}