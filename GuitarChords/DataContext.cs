using GuitarChords.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace GuitarChords
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Chord> Chords { get; set; }
        //public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //modelBuilder.Entity<Chord>(c =>
        //    {
        //        c.Property(x => x.ChordName)
        //        .IsRequired()
        //        .HasMaxLength(30);
        //    });

            //modelBuilder.Entity<User>(u =>
            //{

            //    u.Property(e => e.Login)
            //        .HasMaxLength(50)
            //        .IsRequired();

            //});
        //}
    }
}
