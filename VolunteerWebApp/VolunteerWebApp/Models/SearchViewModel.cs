﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class SearchViewModel
    {
        private ApplicationDbContext _context;

        public SearchViewModel()
        {
            _context = new ApplicationDbContext();
        }

        public  List<List<float>> geoCodes { get; set; }
    }
}