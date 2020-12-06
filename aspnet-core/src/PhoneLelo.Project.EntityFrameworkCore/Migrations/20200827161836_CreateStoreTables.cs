using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneLelo.Project.Migrations
{
    public partial class CreateStoreTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "ProductAdverts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductAdverts",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<long>(
                name: "NeighbourhoodId",
                table: "ProductAdverts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductStoreId",
                table: "ProductAdverts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                table: "ProductAdverts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ProductAdverts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductStores",
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
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    StoreCode = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdverts_CityId",
                table: "ProductAdverts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdverts_NeighbourhoodId",
                table: "ProductAdverts",
                column: "NeighbourhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdverts_ProductStoreId",
                table: "ProductAdverts",
                column: "ProductStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdverts_StateId",
                table: "ProductAdverts",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdverts_UserId",
                table: "ProductAdverts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdverts_Cities_CityId",
                table: "ProductAdverts",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdverts_Neighbourhoods_NeighbourhoodId",
                table: "ProductAdverts",
                column: "NeighbourhoodId",
                principalTable: "Neighbourhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdverts_ProductStores_ProductStoreId",
                table: "ProductAdverts",
                column: "ProductStoreId",
                principalTable: "ProductStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdverts_States_StateId",
                table: "ProductAdverts",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdverts_AbpUsers_UserId",
                table: "ProductAdverts",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdverts_Cities_CityId",
                table: "ProductAdverts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdverts_Neighbourhoods_NeighbourhoodId",
                table: "ProductAdverts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdverts_ProductStores_ProductStoreId",
                table: "ProductAdverts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdverts_States_StateId",
                table: "ProductAdverts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdverts_AbpUsers_UserId",
                table: "ProductAdverts");

            migrationBuilder.DropTable(
                name: "ProductStores");

            migrationBuilder.DropIndex(
                name: "IX_ProductAdverts_CityId",
                table: "ProductAdverts");

            migrationBuilder.DropIndex(
                name: "IX_ProductAdverts_NeighbourhoodId",
                table: "ProductAdverts");

            migrationBuilder.DropIndex(
                name: "IX_ProductAdverts_ProductStoreId",
                table: "ProductAdverts");

            migrationBuilder.DropIndex(
                name: "IX_ProductAdverts_StateId",
                table: "ProductAdverts");

            migrationBuilder.DropIndex(
                name: "IX_ProductAdverts_UserId",
                table: "ProductAdverts");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "ProductAdverts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductAdverts");

            migrationBuilder.DropColumn(
                name: "NeighbourhoodId",
                table: "ProductAdverts");

            migrationBuilder.DropColumn(
                name: "ProductStoreId",
                table: "ProductAdverts");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "ProductAdverts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductAdverts");
        }
    }
}
