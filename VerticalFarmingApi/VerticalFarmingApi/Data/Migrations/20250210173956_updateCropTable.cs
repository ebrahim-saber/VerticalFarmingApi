using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerticalFarmingApi.Migrations
{
    /// <inheritdoc />
    public partial class updateCropTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crops_Farms_FarmId",
                table: "Crops");

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Crops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_Farms_FarmId",
                table: "Crops",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crops_Farms_FarmId",
                table: "Crops");

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Crops",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_Farms_FarmId",
                table: "Crops",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");
        }
    }
}
