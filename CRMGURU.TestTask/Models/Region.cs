using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMGURU.TestTask.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Region(string name)
        {
            Name = name;
        }

        public Region()
        {
           
        }
    }
}