using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlogImage = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "BlogImage", "Description", "Name" },
                values: new object[] { 39, "uploads/placeholder.jpg", "Description for category number 0", "Category number: 0" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "BlogImage", "Description", "Name" },
                values: new object[] { 40, "uploads/placeholder.jpg", "Description for category number 1", "Category number: 1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "BlogImage", "Description", "Name" },
                values: new object[] { 41, "uploads/placeholder.jpg", "Description for category number 2", "Category number: 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
