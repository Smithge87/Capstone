using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Categories> CategoryList { get; set; }
        public List<string> OrgNames { get; set; }
        public List<float>userLocation { get; set; }
        public List<Distances> Distances { get; set; }
        public List<ApplicationUser> Organizations { get; set; }

        [Display(Name = "Filter by Category")]
        public string CategoryFilter { get; set; }
        [Display(Name = "Filter by Organization")]
        public string OrganizationFilter { get; set; }
        [Display(Name = "Filter by Keyword")]
        public string KeywordFilter { get; set; }
        [Display(Name = "Filter by Distance")]
        public string DistanceFilter { get; set; }
    }
}