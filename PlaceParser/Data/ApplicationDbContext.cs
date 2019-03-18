using Microsoft.EntityFrameworkCore;
using PlaceParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceParser.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceType> PlaceTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PlaceParser;Trusted_Connection=True;");
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
