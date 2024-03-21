using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_LearningProject.Migrations
{
    /// <inheritdoc />
    public partial class NewCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_categoryId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_categoryId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "Category_Id",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Category_Id",
                table: "Courses",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_Category_Id",
                table: "Courses",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_Category_Id",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Category_Id",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Category_Id",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_categoryId",
                table: "Courses",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_categoryId",
                table: "Courses",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
