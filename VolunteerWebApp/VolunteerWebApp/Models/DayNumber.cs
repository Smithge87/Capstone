using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class DayNumber
    {
        [Key]
        public int ID { get; set; }
        public string Day { get; set; }
    }
}