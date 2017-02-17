using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class VolunteerSettings
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public bool CanContact { get; set; }
        public bool CanSee { get; set; }
        public bool CanRefer { get; set; }
    }
}