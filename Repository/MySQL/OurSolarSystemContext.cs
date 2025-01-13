using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MySQL
{
    public class OurSolarSystemContext(DbContextOptions<OurSolarSystemContext> options) : DbContext(options)
    {
        public DbSet<SolarSystemBarycenter> SolarSystemBaryCenter { get; set; }
        public DbSet<Barycenter> Barycenters { get; set; }
        public DbSet<Star> Sun { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Moon> Moons { get; set; }
        public DbSet<UserFavoriteSatellite> UserFavoriteSatellites { get; set; }
        public DbSet<ArtificialSatellite> ArtificialSatellites { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EphemerisBarycenter> EphemerisBarycenters { get; set; }
        public DbSet<EphemerisPlanet> EphemerisPlanets { get; set; }
        public DbSet<EphemerisMoon> EphemerisMoons { get; set; }
        public DbSet<EphemerisSun> EphemerisSun { get; set; }
        public DbSet<EphemerisArtificialSatellite> EphemerisArtificialSatellites { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>()
            .HasAlternateKey(p => p.HorizonId);

            modelBuilder.Entity<DistanceResult>(entity =>
            {
                entity.HasNoKey(); // Marking this entity as keyless
            });

            modelBuilder.Entity<Planet>()
            .HasAlternateKey(p => p.HorizonId);

            modelBuilder.Entity<Moon>()
            .HasAlternateKey(m => m.HorizonId);

            modelBuilder.Entity<Barycenter>()
            .HasAlternateKey(b => b.HorizonId);

            modelBuilder.Entity<SolarSystemBarycenter>()
            .HasAlternateKey(s => s.HorizonId);

            modelBuilder.Entity<ArtificialSatellite>()
            .HasAlternateKey(s => s.NoradId);

            modelBuilder.Entity<EphemerisPlanet>()
            .HasIndex(e => new { e.DateTime, e.Id });

            modelBuilder.Entity<EphemerisMoon>()
            .HasIndex(e => new { e.DateTime, e.Id });

            modelBuilder.Entity<EphemerisSun>()
            .HasIndex(e => new { e.DateTime, e.Id });

            modelBuilder.Entity<EphemerisBarycenter>()
            .HasIndex(e => new { e.DateTime, e.Id });


            modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Username)
            .IsUnique();
        }
    }
}
