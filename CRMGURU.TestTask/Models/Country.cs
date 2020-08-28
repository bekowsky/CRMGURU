using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMGURU.TestTask.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public double Area { get; set; }

        public int Population { get; set; }

        public int CityId { get; set; }

        public int RegionId { get; set; }

        public Country (ApiInfo info, int cityid, int regionid)
        {
            Name = info.name;
            Code = info.alpha3Code;
            Area = Convert.ToDouble( info.area.Replace('.',','));
            Population = Convert.ToInt32( info.population);
            CityId = cityid;
            RegionId = regionid;
        }

        public Country()
        {

        }

    }
}