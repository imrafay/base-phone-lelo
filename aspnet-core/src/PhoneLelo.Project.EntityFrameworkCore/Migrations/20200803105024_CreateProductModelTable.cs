using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneLelo.Project.Migrations
{
    public partial class CreateProductModelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductModels",
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
                    ProductCompanyId = table.Column<long>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    NetworkTechnology = table.Column<string>(nullable: true),
                    DisplaySize = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    Features = table.Column<string>(nullable: true),
                    MemoryInternal = table.Column<string>(nullable: true),
                    MainCameraSingle = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    PlatformOS = table.Column<string>(nullable: true),
                    SelfieCameraFeature = table.Column<string>(nullable: true),
                    Sound = table.Column<string>(nullable: true),
                    Battery = table.Column<string>(nullable: true),
                    BatteryTalkTime = table.Column<string>(nullable: true),
                    LaunchAnnouncedYear = table.Column<string>(nullable: true),
                    DisplayResolution = table.Column<string>(nullable: true),
                    FeaturesSensors = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductModels_ProductCompanies_ProductCompanyId",
                        column: x => x.ProductCompanyId,
                        principalTable: "ProductCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductModels_ProductCompanyId",
                table: "ProductModels",
                column: "ProductCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductModels");
        }
    }
}
