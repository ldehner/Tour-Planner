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
            if (!options.IsConfigured)
            {
                options.UseNpgsql("Server=hattie.db.elephantsql.com;Database=sdgyomkk;Port=5432;User Id=sdgyomkk;Password=PYQhfkmfL4sBlO419OgEPuSkHEtQiuIp");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<Tours> Tours { get; set; }
            public DbSet<Logs> Logs { get; set; }
        public DbSet<Adresses> Adress { get; set; }
    }
}
