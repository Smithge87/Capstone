using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VolunteerWebApp.Models;

namespace VolunteerWebApp.Controllers
{
    public class OpportunityController : Controller
    {
        public ApplicationDbContext _context;
        public OpportunityController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Opportunity
        public ActionResult Index()
        {

            return View();
        }
    }
}