﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class Interest
    {
        [Key]
        public int ID { get; set; }
        public int OpportunityId { get; set; }
        public int VolunteerId { get; set; }
        public string InterestLevel { get; set; }
    }
}