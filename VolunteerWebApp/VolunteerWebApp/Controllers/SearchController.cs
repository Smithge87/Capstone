using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using VolunteerWebApp.Models;
using System.Device.Location;



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
            GetLocationProperty();

            List<ApplicationUser> orgs = new List<ApplicationUser>();
            List<Information> addresses = new List<Information>();
            foreach (var address in _context.Address)
            {
                addresses.Add(address);
            }
            foreach (var user in _context.Users)
            {
                if (user.OrganizationName != null)
                {
                    if (user.LogoImgSrc == null)
                    {
                        string exitLocation = user.ProfilePhoto.Substring(10);
                        Resize((@"C:\Users\Jack\Desktop\Projects\Capstone\VolunteerWebApp\VolunteerWebApp" + user.ProfilePhoto), @"C:\Users\Jack\Desktop\Projects\Capstone\VolunteerWebApp\VolunteerWebApp/mapImages/" + exitLocation, .5);
                        string pinImage = "../mapImages/" + exitLocation;
                        user.LogoImgSrc = pinImage;
                    }
                    if (user.GeoLat == null && user.GeoLong == null)
                    {
                        try
                        {
                            var currentAddress = addresses.FirstOrDefault(m => m.UserId == user.Email);
                            string address = currentAddress.StreetAddress + " " + currentAddress.City + " " + currentAddress.State + " " + currentAddress.Zipcode;
                            List<float> geoBit = getGeocode(address);
                            if (geoBit.Count > 1)
                            {
                                user.GeoLat = geoBit[0].ToString();
                                user.GeoLong = geoBit[1].ToString();
                            }
                        }
                        catch { };
                    }
                    if(user.GeoLat != null && user.GeoLong != null)
                    {
                        orgs.Add(user);
                    }
                }
            }
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
            //USE FOR INTEGRATING USER LOCATION... Later
            //var currentUserName = User.Identity.Name;
            //var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            //var userInfo = _context.Address.FirstOrDefault(m => m.UserId == currentUser.Email);
            //List<float> userGeo = getGeocode(userInfo.StreetAddress + " " + userInfo.City + " " + userInfo.State + " " + userInfo.Zipcode);
            List<Opportunity> opps = new List<Opportunity>();
            foreach (var opp in _context.Opportunity)
            {
                if (opp.GeoLat == null && opp.GeoLong == null)
                {
                    string address = opp.StreetAddress + " " + opp.City + " " + opp.State + " " + opp.Zipcode;
                    List<float> geoBit = getGeocode(address);
                    if (geoBit.Count > 1)
                    {
                        opp.GeoLat = geoBit[0].ToString();
                        opp.GeoLong = geoBit[1].ToString();
                        opps.Add(opp);
                    }
                }
                else if (opp.GeoLat !=null && opp.GeoLong != null)
                {
                    opps.Add(opp);
                }

            }
            _context.SaveChanges();
            var viewModel = new SearchViewModel()
            {
                cleanOpps = opps,
                //userLocation = userGeo,
                CategoryList = categoryList,
                OrgNames = orgNames,
                Distances = distanceList,
                Organizations = orgs
            };
            return View(viewModel);
        }
        [ActionName("FilterSearch")]
        public ActionResult Index(SearchViewModel model)
        {
            if(model.CategoryFilter == null && model.DistanceFilter == null && model.KeywordFilter == null && model.OrganizationFilter == null)
            {
                return RedirectToAction("Index");
            }
            List<ApplicationUser> orgs = new List<ApplicationUser>();
            List<Information> addresses = new List<Information>();
            foreach (var address in _context.Address)
            {
                addresses.Add(address);
            }
            foreach (var user in _context.Users)
            {
                if (user.OrganizationName != null && user.GeoLat != null && user.GeoLong != null)
                {
                        orgs.Add(user);
                }
            }
            var currentUserName = User.Identity.Name;
            var currentUser = _context.Users.FirstOrDefault(m => m.UserName == currentUserName);
            List<Opportunity> filteredOpps = _context.Opportunity.ToList();
            if (model.CategoryFilter != null)
            {
                var tempcat = Int32.Parse(model.CategoryFilter);
                var category = _context.Categories.SingleOrDefault(m => m.ID == tempcat);
                var justCat = category.Category;
                    for (int i = (filteredOpps.Count - 1); i >= 0; i--)
                    {
                        if (filteredOpps[i].Category == justCat)
                        {
                        }
                        else
                        {
                            filteredOpps.RemoveAt(i);
                        }
                    }
                
            }
            if (model.OrganizationFilter != null && filteredOpps.Count >0)
            {
                for (int i = (filteredOpps.Count - 1); i >= 0; i--)
                {
                    if (filteredOpps[i].OrganizationHostName == model.OrganizationFilter)
                    {
                    }
                    else
                    {
                        filteredOpps.RemoveAt(i);
                    }
                }
            }
            if (model.KeywordFilter != null && filteredOpps.Count >0)
            {
                for (int i = (filteredOpps.Count - 1); i >= 0; i--)
                {
                    if (filteredOpps[i].AboutOpportunity.Contains(model.KeywordFilter))
                    {
                    }
                    else
                    {
                        filteredOpps.RemoveAt(i);
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
                if (filteredOpps.Count>0 && wantedOpps.Count>0)
                {
                    for (int i = (filteredOpps.Count -1); i>=0;i--)
                    {
                        if (wantedOpps.Contains(filteredOpps[i]))
                        {
                        }
                        else
                        {
                            filteredOpps.RemoveAt(i);
                        }
                    }
                }
            }
            var distanceList = _context.Distances.ToList();
            var categoryList = _context.Categories.ToList();
            var userInfo = _context.Address.FirstOrDefault(m => m.UserId == currentUser.Email);
            List<float> userGeo = getGeocode(userInfo.StreetAddress + " " + userInfo.City + " " + userInfo.State + " " + userInfo.Zipcode);
            List<Opportunity> opps = new List<Opportunity>();
            foreach (var opp in filteredOpps)
            {
                if(opp.GeoLat!= null && opp.GeoLong != null)
                {
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
                Distances = distanceList,
                Organizations = orgs
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
                if (j == "0" || j == "1" || j == "2" || j == "3" || j == "4" || j == "5" || j == "6" || j == "7" || j == "8" || j == "9" || j == "." || j == "-")
                {
                    string newData = cleanData + j;
                    cleanData = newData;
                }
            }
            return cleanData;
        }

        public void Resize(string imageFile, string outputFile, double scaleFactor)
        {
            using (var srcImage = Image.FromFile(imageFile))
            {
                var newWidth = (int)(40);
                var newHeight = (int)(40);
                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                    newImage.Save(outputFile);
                }
                var imagePath = imageFile.Split('/');
            }
        }
        public void GetLocationProperty()
        { 
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            watcher.TryStart(false, TimeSpan.FromMilliseconds(3000));

            GeoCoordinate coord = watcher.Position.Location;
            var banana = "abana";
        }
    }


}

