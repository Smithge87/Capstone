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
        public string CanContact { get; set; }
        public string CanSee { get; set; }
        public string CanRefer { get; set; }
    }
}