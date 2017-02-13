using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class OrganizationProfileViewModel
    {
        private ApplicationDbContext _context;

        public OrganizationProfileViewModel()
        {
            _context = new ApplicationDbContext();
        }

        public ApplicationUser ApplicationUser { get; set; }
        public Information Information { get; set; }
        public State State { get; set; }
    }
}