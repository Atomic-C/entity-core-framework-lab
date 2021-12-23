using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicLib_DataAccess.Migrations
{
    public partial class revertedBackCategoryIdInBookTableAfterTestItWorks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_Change_Me_After_Test_Category_Id",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Change_Me_After_Test_Category_Id",
                table: "Books",
                newName: "Category_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Change_Me_After_Test_Category_Id",
                table: "Books",
                newName: "IX_Books_Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_Category_Id",
                table: "Books",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_Category_Id",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "Books",
                newName: "Change_Me_After_Test_Category_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Category_Id",
                table: "Books",
                newName: "IX_Books_Change_Me_After_Test_Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_Change_Me_After_Test_Category_Id",
                table: "Books",
                column: "Change_Me_After_Test_Category_Id",
                principalTable: "Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
