﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VolunteerWebApp.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string StateId { get; set; }
        public string Zipcode { get; set; }
    }
}