using Microsoft.EntityFrameworkCore.Migrations;

namespace BestBooks.BookMicroservice.DataAccess.Migrations
{
    public partial class UpdateV16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageURL",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageURL",
                table: "Book");
        }
    }
}
