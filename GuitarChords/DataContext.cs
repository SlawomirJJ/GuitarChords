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
    }
}
