using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicLib_DataAccess.Migrations
{
    public partial class addRawCategoryToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Fluent_Category VALUES ('Cat 1')");
            migrationBuilder.Sql("INSERT INTO Fluent_Category VALUES ('Cat 2')");
            migrationBuilder.Sql("INSERT INTO Fluent_Category VALUES ('Cat 3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
