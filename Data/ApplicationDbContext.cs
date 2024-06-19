using HandmadeShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HandmadeShop.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Blanket", DisplayOrder = 1 },
				new Category { Id = 2, Name = "Toy", DisplayOrder = 2 },
				new Category { Id = 3, Name = "Beanie", DisplayOrder = 3 }
				);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Baby Blanket",
                    Description = "Crochet Baby blanket",
                    Color = "Blue",
                    Size = "One Size",
                    ListPrice = 60,
                    Price2 = 50,
                    Price5 = 40,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Southpark Blanket",
                    Description = "Theme Southpark crochet blanket",
                    Color = "Multicolor",
                    Size = "One Size",
                    ListPrice = 90,
                    Price2 = 80,
                    Price5 = 70,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Bear Beanie",
                    Description = "Crochet Baby beanie-Bear",
                    Color = "Brown",
                    Size = "Newborn",
                    ListPrice = 30,
                    Price2 = 25,
                    Price5 = 20,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Elephant Beanie",
                    Description = "Crochet Baby beanie-Elephant",
                    Color = "Gray",
                    Size = "Toddler",
                    ListPrice = 40,
                    Price2 = 36,
                    Price5 = 32,
                    CategoryId = 3,
                    ImageUrl = ""
                });
        }
	}
}
