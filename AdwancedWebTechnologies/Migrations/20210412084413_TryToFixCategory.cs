using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedWebTechnologies.Migrations
{
    public partial class TryToFixCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Producers_ProducerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ParrentCategoryId",
                table: "Categories",
                newName: "ParrentCategoryCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParrentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParrentCategoryCategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "ProducerId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryCategoryId",
                table: "Categories",
                column: "ParrentCategoryCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Producers_ProducerId",
                table: "Products",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "ProducerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Producers_ProducerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ParrentCategoryCategoryId",
                table: "Categories",
                newName: "ParrentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParrentCategoryCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParrentCategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "ProducerId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryId",
                table: "Categories",
                column: "ParrentCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Producers_ProducerId",
                table: "Products",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "ProducerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
