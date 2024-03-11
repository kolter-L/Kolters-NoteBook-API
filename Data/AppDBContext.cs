using NoteBook.Data;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace NoteBook.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Note[] NotesToSeed = new Note[5];

            for (int i = 1; i <= 5; i++)
            {
                NotesToSeed[i - 1] = new Note
                {
                    Id = i,
                    Title = $"Post {i}",
                    Content = $"This is Post {i}"
                };

            }

            modelBuilder.Entity<Note>().HasData(NotesToSeed);

        }
    }
}
