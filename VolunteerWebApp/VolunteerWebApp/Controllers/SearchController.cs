using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using VolunteerWebApp.Models;

namespace VolunteerWebApp.Controllers
{
    public class SearchController : Controller
    {
        public ApplicationDbContext _context;
        public SearchController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Search
        public ActionResult Index()
        {
            var categoryList = _context.Categories.ToList();
            var distanceList = _context.Distances.ToList();
            List<string> orgNames = new List<string>();
            foreach (var user in _context.Users)
            {
                if (user.OrganizationName != null)
                {
                    orgNames.Add(user.OrganizationName);
                }
            }
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            var userInfo = _context.Address.FirstOrDefault(m => m.UserId == currentUser.Email);
            List<float> userGeo = getGeocode(userInfo.StreetAddress + " " + userInfo.City + " " + userInfo.State + " " + userInfo.Zipcode);
            List<Opportunity> opps = new List<Opportunity>();
            List<List<float>> geoCoded = new List<List<float>>();
            foreach (var opp in _context.Opportunity)
            {

                string address = opp.StreetAddress + " " + opp.City + " " + opp.State + " " + opp.Zipcode;
                List<float> geoBit = getGeocode(address);
                if (geoBit.Count > 1)
                {
                    opp.GeoLocation = geoBit;
                    opps.Add(opp);
                }
            }
            var viewModel = new SearchViewModel()
            {
                cleanOpps = opps,
                userLocation = userGeo,
                CategoryList = categoryList,
                OrgNames = orgNames,
                Distances = distanceList
            };
            return View(viewModel);
        }
        [ActionName ("FilterSearch")]
        public ActionResult Index (SearchViewModel model)
        {
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            List<Opportunity> filteredOpps = new List<Opportunity>();
            if (model.CategoryFilter != null)
            {
                var tempcat = Int32.Parse(model.CategoryFilter);
                var category = _context.Categories.SingleOrDefault(m => m.ID == tempcat);
                var justCat = category.Category;
                foreach (var opportunity in _context.Opportunity)
                {
                    if (opportunity.Category == justCat)
                    {
                        filteredOpps.Add(opportunity);
                    }
                }
            }
            if (model.OrganizationFilter != null)
            {
                foreach (var opportunity in _context.Opportunity)
                {
                    if (opportunity.OrganizationHostName == model.OrganizationFilter)
                    {
                        filteredOpps.Add(opportunity);
                    }
                }
            }
            if (model.KeywordFilter != null)
            {
                foreach( var opportunity in _context.Opportunity)
                {
                    if (opportunity.AboutOpportunity.Contains(model.KeywordFilter) == true )
                    {
                        filteredOpps.Add(opportunity);
                    }
                }
            }
            if (model.DistanceFilter != null)
            {
                var tempDist = Int32.Parse(model.DistanceFilter);
                var distance = _context.Distances.SingleOrDefault(m => m.ID == tempDist);
                var justDist = distance.Distance;
                var intDist = float.Parse(cleanXML(justDist));
                List<Opportunity> wantedOpps = getDistances(currentUser, intDist);
                foreach (var opp in wantedOpps)
                {
                    filteredOpps.Add(opp);
                }
            }
            var distanceList = _context.Distances.ToList();
            var categoryList = _context.Categories.ToList();
            var userInfo = _context.Address.FirstOrDefault(m => m.UserId == currentUser.Email);
            List<float> userGeo = getGeocode(userInfo.StreetAddress + " " + userInfo.City + " " + userInfo.State + " " + userInfo.Zipcode);
            List<Opportunity> opps = new List<Opportunity>();
            List<List<float>> geoCoded = new List<List<float>>();
            foreach (var opp in filteredOpps )
            {

                string address = opp.StreetAddress + " " + opp.City + " " + opp.State + " " + opp.Zipcode;
                List<float> geoBit = getGeocode(address);
                if (geoBit.Count > 1)
                {
                    opp.GeoLocation = geoBit;
                    opps.Add(opp);
                }
            }
            List<string> orgNames = new List<string>();
            foreach (var user in _context.Users)
            {
                if (user.OrganizationName != null)
                {
                    orgNames.Add(user.OrganizationName);
                }
            }
            var viewModel = new SearchViewModel()
            {
                cleanOpps = opps,
                userLocation = userGeo,
                CategoryList = categoryList,
                OrgNames = orgNames,
                Distances = distanceList
            };
            return View("Index", viewModel);
        }
        public List<Opportunity> getDistances(ApplicationUser user, float distanceConstraint)
        {
            List<Opportunity> wantedOpps = new List<Opportunity>();
            var userInfo = _context.Address.FirstOrDefault(m => m.UserId == user.Email);
            var rootUrl = "https://maps.googleapis.com/maps/api/directions/json?origin=";
            foreach (var opp in _context.Opportunity)
            {
                try
                {
                    var startSt = userInfo.StreetAddress.Replace(" ", "+") + "+";
                    var startCity = userInfo.City.Replace(" ", "+") + "+";
                    var startZip = userInfo.Zipcode;
                    var endSt = opp.StreetAddress.Replace(" ", "+") + "+";
                    var endCity = opp.City.Replace(" ", "+") + "+";
                    var endZip = opp.Zipcode;
                    var URL = rootUrl + startSt + startCity + startZip + "&destination=" + endSt + endCity + endZip + "&key=AIzaSyABln82MFoySBkjQPoJjeVYgeoK_R_RKPE";
                    var request = WebRequest.Create(URL);
                    var response = request.GetResponse();
                    var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    var distance = JObject.Parse(stream.ReadToEnd());
                    var readAble = distance.ToString();
                    var answer = distance["routes"][0]["legs"][0]["distance"]["text"].ToString();
                    var cleanDistance = float.Parse(cleanXML(answer));
                    if (cleanDistance < distanceConstraint)
                    {
                        wantedOpps.Add(opp);
                    }
                }
                catch { }
            }
                return wantedOpps;
        }
        public List<float> getGeocode(string address)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());
            try
            {
                var result = xdoc.Element("GeocodeResponse").Element("result");
                var locationElement = result.Element("geometry").Element("location");
                float lat = (float.Parse(cleanXML(locationElement.Element("lat").ToString())));
                float lng = (float.Parse(cleanXML(locationElement.Element("lng").ToString())));
                List<float> geo = new List<float>() { lat, lng };
                return geo;
            }
            catch
            {
                List<float> blank = new List<float>();
                return blank;
            }
        }
        public string cleanXML(string data)
        {
            string cleanData = "";
            foreach (char i in data)
            {
                string j = i.ToString();
                if (j =="0"|| j == "1" || j == "2" || j == "3" || j == "4" || j == "5" || j == "6" || j == "7" || j == "8" || j == "9" || j == "." || j == "-" )
                {
                    string newData = cleanData + j;
                    cleanData = newData;
                }
            }
            return cleanData;
        }

    }
}

