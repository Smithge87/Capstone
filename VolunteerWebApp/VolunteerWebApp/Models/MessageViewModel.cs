using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class MessageViewModel
    {
        private ApplicationDbContext _context;

        public MessageViewModel()
        {
            _context = new ApplicationDbContext();
        }
        public ApplicationUser SendFrom { get; set; }
        public List<ApplicationUser> SendList { get; set; }


        public string senderId { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }

    }
}