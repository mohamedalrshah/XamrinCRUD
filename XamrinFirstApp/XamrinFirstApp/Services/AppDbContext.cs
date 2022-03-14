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
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyDB.db3");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
