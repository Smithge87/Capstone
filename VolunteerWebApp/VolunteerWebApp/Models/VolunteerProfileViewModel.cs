﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class VolunteerProfileViewModel
    {
        private ApplicationDbContext _context;

        public VolunteerProfileViewModel()
        {
            _context = new ApplicationDbContext();
        }

        public ApplicationUser ApplicationUser { get; set; }
        public Skill Skill { get; set; }
        public Information Information { get; set; }
        public VolunteerSettings VolunteerSettings { get ;set;}
        public State State { get; set; }
        public List<Opportunity> MyOpportunities { get; set; }

    }
}