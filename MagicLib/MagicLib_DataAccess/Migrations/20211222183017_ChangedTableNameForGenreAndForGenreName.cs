using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicLib_DataAccess.Migrations
{
    public partial class ChangedTableNameForGenreAndForGenreName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre_table");

            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genre_table",
                newName: "Genre_name_column");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre_table",
                table: "Genre_table",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre_table",
                table: "Genre_table");

            migrationBuilder.RenameTable(
                name: "Genre_table",
                newName: "Genres");

            migrationBuilder.RenameColumn(
                name: "Genre_name_column",
                table: "Genres",
                newName: "GenreName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "GenreId");
        }
    }
}
