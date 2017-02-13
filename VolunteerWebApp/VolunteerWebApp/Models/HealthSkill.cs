﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class HealthSkill
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public string SkillLevel { get; set; }
    }
}