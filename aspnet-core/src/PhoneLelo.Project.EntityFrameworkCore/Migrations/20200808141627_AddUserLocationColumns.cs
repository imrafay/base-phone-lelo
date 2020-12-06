using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneLelo.Project.Migrations
{
    public partial class AddUserLocationColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocationFilled",
                table: "AbpUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "NeighbourhoodId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_CityId",
                table: "AbpUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NeighbourhoodId",
                table: "AbpUsers",
                column: "NeighbourhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_StateId",
                table: "AbpUsers",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Cities_CityId",
                table: "AbpUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Neighbourhoods_NeighbourhoodId",
                table: "AbpUsers",
                column: "NeighbourhoodId",
                principalTable: "Neighbourhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_States_StateId",
                table: "AbpUsers",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Cities_CityId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Neighbourhoods_NeighbourhoodId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_States_StateId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_CityId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_NeighbourhoodId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_StateId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "IsLocationFilled",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "NeighbourhoodId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "AbpUsers");
        }
    }
}
