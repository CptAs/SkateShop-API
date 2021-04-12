using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedWebTechnologies.Migrations
{
    public partial class TryToFixCategoryv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParrentCategoryCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParrentCategoryCategoryId",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParrentCategoryCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParrentCategoryCategoryId",
                table: "Categories",
                column: "ParrentCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryCategoryId",
                table: "Categories",
                column: "ParrentCategoryCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
