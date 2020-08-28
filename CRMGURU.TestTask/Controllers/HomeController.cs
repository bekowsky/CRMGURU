using CRMGURU.TestTask.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CRMGURU.TestTask.Controllers
{
    public class HomeController : Controller
    {
       DataContext db = new DataContext();
        public ApiInfo  GetApiInfo(string name)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/name/" + name);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string data;
            List<ApiInfo> info = new List<ApiInfo> ();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                info = JsonConvert.DeserializeObject<List<ApiInfo>>(data);
            }
            return info.First();
        }

        public PartialViewResult InfoCountry(string name)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/name/" + name);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string data;
                List<ApiInfo> info = new List<ApiInfo>();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }
                    data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                    info = JsonConvert.DeserializeObject<List<ApiInfo>>(data);
                }
                return PartialView(info.First());
            } catch (Exception e)
            {
                return PartialView(null);
            }
        }
        public PartialViewResult AllCountry()
        {
           
                ViewBag.Cities = db.Cities;
            ViewBag.Regions = db.Regions;
                return PartialView(db.Countries);
            
        }

        public void SaveResult(ApiInfo obj)
        {
            
            using (DataContext db = new DataContext())
            {
              int CityId =  db.FindCity(obj.capital);
              int RegionId = db.FindRegion(obj.region);
                Country country = new Country(obj,CityId,RegionId);
                if (db.ContainsCode(country.Code))
                    db.RefreshCountry(country);
                else db.AddCountry(country);
            }
        }
        public ActionResult Index()
        {



            GetApiInfo("Russia");
                return View();
        }
    }
}