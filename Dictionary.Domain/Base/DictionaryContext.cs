using System.Configuration;
using Dictionary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Domain.Base
{
    public class DictionaryContext : DbContext
    {
        public DbSet<Word> Words { get; set; }

        public DbSet<Description> Descriptions { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FullWord>()
                .HasKey(fw => fw.Id);

            modelBuilder.Entity<FullWord>()
                .HasOne(fw => fw.Word)
                .WithMany(w => w.WordDescriptions)
                .HasForeignKey(w => w.WordId);

            modelBuilder.Entity<FullWord>()
                .HasOne(fw => fw.Description)
                .WithMany(d => d.WordDescriptions)
                .HasForeignKey(d => d.DescriptionId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings[1].ConnectionString);
            //optionsBuilder.UseSqlServer("Data Source=LAPTOP-A41SUOC9;Initial Catalog=Dictionary;Integrated Security=SSPI;");
        }
    }
}
