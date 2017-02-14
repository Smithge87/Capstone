using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class PhotoViewModel
    {
        private ApplicationDbContext _context;

        public PhotoViewModel()
        {
            _context = new ApplicationDbContext();
        }
        public ApplicationUser ApplicationUser { get; set; }

        public string PhotoLocation { get; set; }
    }
}