using Microsoft.EntityFrameworkCore.Migrations;

namespace BestBooks.BookMicroservice.DataAccess.Migrations
{
    public partial class UpdateV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookPPublisher_BookPublisherID",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_BookPublisherID",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "BookPublisherID",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublisherID",
                table: "Book",
                column: "PublisherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookPPublisher_PublisherID",
                table: "Book",
                column: "PublisherID",
                principalTable: "BookPPublisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookPPublisher_PublisherID",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_PublisherID",
                table: "Book");

            migrationBuilder.AddColumn<long>(
                name: "BookPublisherID",
                table: "Book",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookPublisherID",
                table: "Book",
                column: "BookPublisherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookPPublisher_BookPublisherID",
                table: "Book",
                column: "BookPublisherID",
                principalTable: "BookPPublisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
