using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VolunteerWebApp.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: Organization
        public ActionResult OrganizationIndex()
        {
            return View();
        }
    }
}