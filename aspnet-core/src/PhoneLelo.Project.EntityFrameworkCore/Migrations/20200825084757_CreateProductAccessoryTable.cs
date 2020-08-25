using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneLelo.Project.Migrations
{
    public partial class CreateProductAccessoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInWarranty",
                table: "ProductAdverts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsKit",
                table: "ProductAdverts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RemaingWarrantyInMonths",
                table: "ProductAdverts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductAdvertAccessories",
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
                    AccessoryType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdvertAccessories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdvertAccessories_ProductAdverts_ProductAdvertId",
                        column: x => x.ProductAdvertId,
                        principalTable: "ProductAdverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdvertAccessories_ProductAdvertId",
                table: "ProductAdvertAccessories",
                column: "ProductAdvertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAdvertAccessories");

            migrationBuilder.DropColumn(
                name: "IsInWarranty",
                table: "ProductAdverts");

            migrationBuilder.DropColumn(
                name: "IsKit",
                table: "ProductAdverts");

            migrationBuilder.DropColumn(
                name: "RemaingWarrantyInMonths",
                table: "ProductAdverts");
        }
    }
}
