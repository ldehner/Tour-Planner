using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Data
{
    public class ToursDataContext : DbContext
    {
        public ToursDataContext()
        {

        }
        public ToursDataContext(DbContextOptions<ToursDataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //if (!options.IsConfigured)
           //{
                //options.UseNpgsql("");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<Tours> Tours { get; set; }
            public DbSet<Logs> Logs { get; set; }
        public DbSet<Adresses> Adress { get; set; }
    }
}
