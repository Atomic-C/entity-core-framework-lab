using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicLib_DataAccess.Migrations
{
    public partial class addedManyToManyFluentBookAndFluentPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_FluentAuthors_FluentAuthorAuthor_Id",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_FluentAuthorAuthor_Id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FluentAuthorAuthor_Id",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "FluentBookFluentAuthor",
                columns: table => new
                {
                    FluentAuthorAuthor_Id = table.Column<int>(type: "int", nullable: false),
                    FluentBookBook_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluentBookFluentAuthor", x => new { x.FluentAuthorAuthor_Id, x.FluentBookBook_Id });
                    table.ForeignKey(
                        name: "FK_FluentBookFluentAuthor_FluentAuthors_FluentAuthorAuthor_Id",
                        column: x => x.FluentAuthorAuthor_Id,
                        principalTable: "FluentAuthors",
                        principalColumn: "Author_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FluentBookFluentAuthor_FluentBooks_FluentBookBook_Id",
                        column: x => x.FluentBookBook_Id,
                        principalTable: "FluentBooks",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FluentBookFluentAuthor_FluentBookBook_Id",
                table: "FluentBookFluentAuthor",
                column: "FluentBookBook_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FluentBookFluentAuthor");

            migrationBuilder.AddColumn<int>(
                name: "FluentAuthorAuthor_Id",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_FluentAuthorAuthor_Id",
                table: "Books",
                column: "FluentAuthorAuthor_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_FluentAuthors_FluentAuthorAuthor_Id",
                table: "Books",
                column: "FluentAuthorAuthor_Id",
                principalTable: "FluentAuthors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
