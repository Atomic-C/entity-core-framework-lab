using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicLib_DataAccess.Migrations
{
    public partial class addedFKAnnotationToAuthorBookMT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookMT_Authors_Author_Id1",
                table: "AuthorBookMT");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookMT_Books_Book_Id1",
                table: "AuthorBookMT");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBookMT_Author_Id1",
                table: "AuthorBookMT");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBookMT_Book_Id1",
                table: "AuthorBookMT");

            migrationBuilder.DropColumn(
                name: "Author_Id1",
                table: "AuthorBookMT");

            migrationBuilder.DropColumn(
                name: "Book_Id1",
                table: "AuthorBookMT");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookMT_Book_Id",
                table: "AuthorBookMT",
                column: "Book_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookMT_Authors_Author_Id",
                table: "AuthorBookMT",
                column: "Author_Id",
                principalTable: "Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookMT_Books_Book_Id",
                table: "AuthorBookMT",
                column: "Book_Id",
                principalTable: "Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookMT_Authors_Author_Id",
                table: "AuthorBookMT");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookMT_Books_Book_Id",
                table: "AuthorBookMT");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBookMT_Book_Id",
                table: "AuthorBookMT");

            migrationBuilder.AddColumn<int>(
                name: "Author_Id1",
                table: "AuthorBookMT",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Book_Id1",
                table: "AuthorBookMT",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookMT_Author_Id1",
                table: "AuthorBookMT",
                column: "Author_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookMT_Book_Id1",
                table: "AuthorBookMT",
                column: "Book_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookMT_Authors_Author_Id1",
                table: "AuthorBookMT",
                column: "Author_Id1",
                principalTable: "Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookMT_Books_Book_Id1",
                table: "AuthorBookMT",
                column: "Book_Id1",
                principalTable: "Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
