using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class PartialProfileOrganizationViewModel
    {
        private ApplicationDbContext _context;
        public PartialProfileOrganizationViewModel()
        {
            _context = new ApplicationDbContext();
        }
        public ApplicationUser Organization { get; set; }
        public Information OrganizationInfo { get; set; }
        public List<Opportunity> Opportunities { get; set; }

    }
}