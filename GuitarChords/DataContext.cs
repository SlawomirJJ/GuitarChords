using GuitarChords.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace GuitarChords
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Chord> Chords { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chord>(c =>
            {
                c.Property(x => x.ChordName)
                .IsRequired()
                .HasMaxLength(30);
            });

            modelBuilder.Entity<User>(u =>
            {
                u.Property(e => e.CreatedAt)
                    .IsRequired();

                u.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsRequired();

                u.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                u.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                u.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsRequired();

                u.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsRequired();

            });
        }
    }
}
