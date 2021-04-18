using EntityFrameworkWithJson.Entity;

using Microsoft.EntityFrameworkCore;

using System.Text.Json;

namespace EntityFrameworkWithJson.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TestDatabase.db");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Invoice>(x =>
            {
                x.HasKey(x => x.Id);

                x.Property(x => x.WebhookData)
                    .HasConversion(j => j.GetRawText(), v => JsonDocument.Parse(v, default).RootElement);
            });
        }
    }
}
