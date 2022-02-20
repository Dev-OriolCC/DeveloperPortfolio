using Microsoft.EntityFrameworkCore;
using Shared.Models;


namespace Server.Data
{
    public class AppDBContext : DbContext
    {
        // Name for table on DB - References to Shared/Models/Category
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        //--------
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Model builder to use methods base.* & call Model(Category)
            base.OnModelCreating(modelBuilder);

            #region seed categories
            // Seed database
            // [Category]
            Category[] categoriesToSeed = new Category[3]; // Categories array
            // Fill categories array with data.
            for (int i = 0; i < 3; i++)
            {
                categoriesToSeed[i] = new Category
                {
                    CategoryID = i+39,
                    BlogImage = "uploads/placeholder.jpg",
                    Name = $"Category number: {i}",
                    Description = $"Description for category number {i}",
                };
            }
            // Call the category entity (Model) and fill with categories seeder(categoriesToSeed).
            modelBuilder.Entity<Category>().HasData(categoriesToSeed);
            #endregion

            modelBuilder.Entity<Post>(
                entity =>
                {
                    entity.HasOne(post => post.Category)
                    .WithMany(category => category.Posts)
                    .HasForeignKey("CategoryID"); 
                }
                );
            #region seed posts
            Post[] postsToSeed = new Post[10]; // Posts Array
            // Array of categories ID'S
            int[] categories_id = { 39, 40, 41 };
            //  Fill posts array with dataa..
            for (int i = 1; i <= 10; i++)
            {
                Random random = new Random();
                int temp_categoryId = random.Next(0, categories_id.Length); // Get a random category id from array
                postsToSeed[i-1] = new Post
                {
                    PostID = i + 100,
                    PostImage = "uploads/placeholder.jpg",
                    Title = $"Post number: {i}",
                    Excerpt = $"This is an example excerpt for post number{i}....",
                    Content = string.Empty,
                    PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm"),
                    IsPublished = true,
                    Author = "Oriol",
                    CategoryID = categories_id[temp_categoryId]
                };
            }

            modelBuilder.Entity<Post>().HasData(postsToSeed);
            #endregion
        }
    }
}
