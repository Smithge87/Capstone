using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            List<List<float>> geoCoded = new List<List<float>>();
            foreach (var opp in _context.Opportunity)
            {
                string address = opp.StreetAddress + " " + opp.City + " " + opp.State + " " + opp.Zipcode;
                List<float> geoBit = getGeocode(address);
                if (geoBit.Count > 1)
                {
                    geoCoded.Add(geoBit);
                }
            }
            var viewModel = new SearchViewModel()
            {
                geoCodes = geoCoded
            };
            return View(viewModel);
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

