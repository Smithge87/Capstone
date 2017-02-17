using System;
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

        public  List<Opportunity> cleanOpps { get; set; }
        public List<float>userLocation { get; set; }
        public string CategoryFilter { get; set; }


    }
}