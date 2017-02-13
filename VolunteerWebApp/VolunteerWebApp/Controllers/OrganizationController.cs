using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VolunteerWebApp.Models;

namespace VolunteerWebApp.Controllers
{
    public class OrganizationController : Controller
    {
        public ApplicationDbContext _context;
        public OrganizationController()
        {
            _context = new ApplicationDbContext();
        }



        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
    }
}