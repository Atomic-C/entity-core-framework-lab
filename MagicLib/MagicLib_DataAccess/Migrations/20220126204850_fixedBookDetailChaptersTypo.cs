using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicLib_DataAccess.Migrations
{
    public partial class fixedBookDetailChaptersTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfCapters",
                table: "BookDetails",
                newName: "NumberOfChapters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfChapters",
                table: "BookDetails",
                newName: "NumberOfCapters");
        }
    }
}
