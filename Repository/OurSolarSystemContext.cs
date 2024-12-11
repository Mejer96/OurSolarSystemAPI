using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository
{
    public class OurSolarSystemContext(DbContextOptions<OurSolarSystemContext> options) : DbContext(options)
    {
        public DbSet<Barycenter> Barycenters { get; set; }
        // public DbSet<Star> Star { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Moon> Moons { get; set; }
        public DbSet<ArtificialSatellite> ArtificialSatellites  { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<EphemerisBarycenter> EphemerisBarycenters { get; set; }
        public DbSet<EphemerisPlanet> EphemerisPlanets { get; set; }
        public DbSet<EphemerisMoon> EphemerisMoons { get; set; }
        public DbSet<EphemerisArtificialSatellite> EphemerisArtificialSatellites { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}
