using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneLelo.Project.Migrations
{
    public partial class CreateProductAdvertTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAdverts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ProductModelId = table.Column<long>(nullable: false),
                    Storage = table.Column<int>(nullable: false),
                    Ram = table.Column<int>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    IsPtaApproved = table.Column<bool>(nullable: false),
                    IsExchangeable = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    IsNegotiable = table.Column<bool>(nullable: false),
                    NegotiableMinValue = table.Column<decimal>(nullable: true),
                    NegotiableMaxValue = table.Column<decimal>(nullable: true),
                    IsSpot = table.Column<bool>(nullable: true),
                    IsDamage = table.Column<bool>(nullable: true),
                    IsFingerSensorWorking = table.Column<bool>(nullable: true),
                    IsFaceSensorWorking = table.Column<bool>(nullable: true),
                    BatteryHealth = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdverts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdverts_ProductModels_ProductModelId",
                        column: x => x.ProductModelId,
                        principalTable: "ProductModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdvertBatteryUsages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ProductAdvertId = table.Column<long>(nullable: false),
                    Hours = table.Column<decimal>(nullable: false),
                    BatteryUsageType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdvertBatteryUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdvertBatteryUsages_ProductAdverts_ProductAdvertId",
                        column: x => x.ProductAdvertId,
                        principalTable: "ProductAdverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdvertImages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ProductAdvertId = table.Column<long>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    ImagePriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdvertImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdvertImages_ProductAdverts_ProductAdvertId",
                        column: x => x.ProductAdvertId,
                        principalTable: "ProductAdverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdvertBatteryUsages_ProductAdvertId",
                table: "ProductAdvertBatteryUsages",
                column: "ProductAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdvertImages_ProductAdvertId",
                table: "ProductAdvertImages",
                column: "ProductAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdverts_ProductModelId",
                table: "ProductAdverts",
                column: "ProductModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAdvertBatteryUsages");

            migrationBuilder.DropTable(
                name: "ProductAdvertImages");

            migrationBuilder.DropTable(
                name: "ProductAdverts");
        }
    }
}
