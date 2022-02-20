using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class AddedThePostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    PostImage = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Excerpt = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 65536, nullable: false),
                    PublishDate = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    CategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 101, "Oriol", 41, "", "This is an example excerpt for post number1....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 1" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 102, "Oriol", 40, "", "This is an example excerpt for post number2....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 2" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 103, "Oriol", 41, "", "This is an example excerpt for post number3....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 3" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 104, "Oriol", 40, "", "This is an example excerpt for post number4....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 4" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 105, "Oriol", 41, "", "This is an example excerpt for post number5....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 5" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 106, "Oriol", 41, "", "This is an example excerpt for post number6....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 6" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 107, "Oriol", 41, "", "This is an example excerpt for post number7....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 7" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 108, "Oriol", 39, "", "This is an example excerpt for post number8....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 8" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 109, "Oriol", 41, "", "This is an example excerpt for post number9....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 9" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Author", "CategoryID", "Content", "Excerpt", "IsPublished", "PostImage", "PublishDate", "Title" },
                values: new object[] { 110, "Oriol", 41, "", "This is an example excerpt for post number10....", true, "uploads/placeholder.jpg", "20/02/2022 02:45", "Post number: 10" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryID",
                table: "Posts",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
