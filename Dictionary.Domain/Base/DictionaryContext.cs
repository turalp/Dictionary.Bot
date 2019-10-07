using System.Configuration;
using Dictionary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Domain.Base
{
    public class DictionaryContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasMany(w => w.Descriptions)
                .WithOne(w => w.Word)
                .HasForeignKey(w => w.WordId)
                .HasPrincipalKey(w => w.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        }
    }
}
