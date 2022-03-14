using Microsoft.EntityFrameworkCore;
using System.IO;
using Xamarin.Essentials;
using XamrinFirstApp.Models;

namespace XamrinFirstApp.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public AppDbContext()
        {
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            //this.Countries.Add(new Country() { Name = "Libya", ShortName = "LY" });
            //this.Countries.Add(new Country() { Name = "Tunis", ShortName = "TN" });
            //this.Cities.Add(new City() { Name = "Zliten", CountryId = 1 });
            //this.Cities.Add(new City() { Name = "Tripoli", CountryId = 1 });
            //this.Blogs.Add(new Blog() { Id = 1, Url = "www.google.com" });
            //this.Blogs.Add(new Blog() { Id = 2, Url = "www.facebook.com" });
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyDB.db3");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
