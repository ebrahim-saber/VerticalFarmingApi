using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerticalFarmingApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crops_Farms_FarmId",
                table: "Crops");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Farms_FarmId",
                table: "Sensors");

            migrationBuilder.DropTable(
                name: "SensorReadings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "Recommendations",
                table: "AIAnalysisResults");

            migrationBuilder.RenameColumn(
                name: "PlantedDate",
                table: "Crops",
                newName: "PlantingDate");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Sensors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Crops",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Crops",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SensorId",
                table: "Crops",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SensorDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorDatas_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorImages_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crops_SensorId",
                table: "Crops",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDatas_SensorId",
                table: "SensorDatas",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorImages_SensorId",
                table: "SensorImages",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_Farms_FarmId",
                table: "Crops",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_Sensors_SensorId",
                table: "Crops",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Farms_FarmId",
                table: "Sensors",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crops_Farms_FarmId",
                table: "Crops");

            migrationBuilder.DropForeignKey(
                name: "FK_Crops_Sensors_SensorId",
                table: "Crops");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Farms_FarmId",
                table: "Sensors");

            migrationBuilder.DropTable(
                name: "SensorDatas");

            migrationBuilder.DropTable(
                name: "SensorImages");

            migrationBuilder.DropIndex(
                name: "IX_Crops_SensorId",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "Crops");

            migrationBuilder.RenameColumn(
                name: "PlantingDate",
                table: "Crops",
                newName: "PlantedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Sensors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sensors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Crops",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Crops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Crops",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recommendations",
                table: "AIAnalysisResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SensorReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorReadings_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorReadings_SensorId",
                table: "SensorReadings",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_Farms_FarmId",
                table: "Crops",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Farms_FarmId",
                table: "Sensors",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
