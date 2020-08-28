using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CRMGURU.TestTask.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DbConnection")
        { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }

        public int FindCity (string name)
        {
            
                foreach (City city in Cities)
                {
                    if (name == city.Name)
                        return city.Id;
                }
 
            
           return AddCity(name);
        }
        public int AddCity(string name)
        {
            
                Cities.Add(new City(name));
            this.SaveChanges();
                return   Cities.Single(x => x.Name == name).Id;
                     
        }


        public int FindRegion(string name)
        {

            foreach (Region region in Regions)
            {
                if (name == region.Name)
                    return region.Id;
            }


            return AddRegion(name);
        }
        public int AddRegion(string name)
        {

            Regions.Add(new Region(name));
            this.SaveChanges();
            return Regions.Single(x => x.Name == name).Id;

        }

        public bool ContainsCode (string name)
        {
            foreach (Country country in Countries)
            {
                if (country.Code == name)
                    return true;
            }
            return false;
        }

        public void RefreshCountry (Country country)
        {
            country.Id = GetCountriesId(country.Code);
            Countries.AddOrUpdate(country);
            SaveChanges();
        }

        public void AddCountry(Country country)
        {
            Countries.AddOrUpdate(country);
            SaveChanges();
        }

        public int GetCountriesId (string code)
        {
            return  Countries.SingleOrDefault (x => x.Code == code).Id;
        }

    }
}