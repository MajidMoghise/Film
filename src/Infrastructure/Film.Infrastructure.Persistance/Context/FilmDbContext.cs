using Film.Domain.Enities;
using Film.Infrastructure.Persistance.Context.ModelsBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Infrastructure.Persistance.Context
{
    public class FilmDbContext: DbContext
    {
        private readonly string _connectionstring;
        public virtual DbSet<Domain.Enities.Film> Films{ get; set; }
        public virtual DbSet<Domain.Enities.Category> Categories { get; set; }

        public FilmDbContext(DbContextOptions<FilmDbContext> option,IConfiguration configuration) : base(option)
        {
            _connectionstring = configuration.GetConnectionString("BloggingDatabase"); 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionstring);

            }
        }
        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");
            modelBuilder.ModelsBuilder();
        }

    }
}
