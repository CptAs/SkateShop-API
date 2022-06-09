using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedWebTechnologies.Migrations
{
    public partial class DataBaseChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParrentCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParrentCategoryId",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParrentCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParrentCategoryId",
                table: "Categories",
                column: "ParrentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryId",
                table: "Categories",
                column: "ParrentCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
