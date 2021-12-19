using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BestBooks.BookMicroservice.DataAccess.Migrations
{
    public partial class UpdateV15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookDiscount",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "Datetime", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "Datetime", nullable: false),
                    BookID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDiscount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookDiscount_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookPrice",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "Datetime", nullable: false),
                    BookID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookPrice_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookDiscount_BookID",
                table: "BookDiscount",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookPrice_BookID",
                table: "BookPrice",
                column: "BookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookDiscount");

            migrationBuilder.DropTable(
                name: "BookPrice");
        }
    }
}
