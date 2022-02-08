using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class AppDBContext : DbContext
    {
        // Name for table on DB - References to Shared/Models/Category
        public DbSet<Category> Categories { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Model builder to use methods base.* & call Model(Category)
            base.OnModelCreating(modelBuilder);
            // Seed database
            Category[] categoriesToSeed = new Category[3]; // Categories array
            // Fill categories array with data.
            for (int i = 0; i < 3; i++)
            {
                categoriesToSeed[i] = new Category
                {
                    CategoryID = i,
                    BlogImage = "uploads/placeholder.jpg",
                    Name = $"Category number: {i}",
                    Description = $"Description for category number {i}",
                };
            }
            // Call the category entity (Model) and fill with categories seeder(categoriesToSeed).
            modelBuilder.Entity<Category>().HasData(categoriesToSeed);
        }
    }
}
