using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class AnimalsAndOwnersDbContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Animal> Animals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=AnimalsAndOwners;Trusted_Connection=True;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .ToTable("Animal")
                .HasOne(a => a.Owner)
                .WithMany()
                .HasForeignKey(a => a.OwnerId);

            modelBuilder.Entity<Owner>()
                .ToTable("Owner");
        }
    }
}
