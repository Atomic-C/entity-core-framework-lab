using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicLib_DataAccess.Migrations
{
    public partial class AddCategoryWithMixOfDataAnnotationAndFluentAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fluent_Category",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_Category", x => x.Category_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fluent_Category");
        }
    }
}
