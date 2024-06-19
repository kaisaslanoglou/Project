using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HandmadeShop.Migrations
{
    /// <inheritdoc />
    public partial class addProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price2 = table.Column<double>(type: "float", nullable: false),
                    Price5 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Color", "Description", "ListPrice", "Price2", "Price5", "Size", "Title" },
                values: new object[,]
                {
                    { 1, "Blue", "Crochet Baby blanket", 60.0, 50.0, 40.0, "One Size", "Baby Blanket" },
                    { 2, "Multicolor", "Theme Southpark crochet blanket", 90.0, 80.0, 70.0, "One Size", "Southpark Blanket" },
                    { 3, "Brown", "Crochet Baby beanie-Bear", 30.0, 25.0, 20.0, "Newborn", "Bear Beanie" },
                    { 4, "Gray", "Crochet Baby beanie-Elephant", 40.0, 36.0, 32.0, "Toddler", "Elephant Beanie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
