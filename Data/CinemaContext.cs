using Microsoft.EntityFrameworkCore;
using Lab_4.Models;

namespace Lab_4.Data
{
    public class CinemaContext : DbContext
    {

        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Place> Places { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var builder = new ConfigurationBuilder();
        //    builder.SetBasePath(Directory.GetCurrentDirectory());
        //    builder.AddJsonFile("appsettings.json");
        //    var config = builder.Build();
        //    string connectionString = config.GetConnectionString("SqlServerConnection");
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
    }
}