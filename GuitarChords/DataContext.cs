using GuitarChords.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace GuitarChords
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Chord> Chords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chord>(c =>
            {
                c.Property(x => x.ChordName)
                .IsRequired()
                .HasMaxLength(30);
            });
        }
    }
}
