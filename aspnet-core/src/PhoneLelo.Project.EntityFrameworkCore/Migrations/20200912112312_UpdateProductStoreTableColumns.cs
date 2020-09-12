using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneLelo.Project.Migrations
{
    public partial class UpdateProductStoreTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "StoreCode",
                table: "ProductStores",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ProductStores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageIconUrl",
                table: "ProductStores",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserProductStores",
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
                    UserId = table.Column<long>(nullable: false),
                    ProductStoreId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProductStores_ProductStores_ProductStoreId",
                        column: x => x.ProductStoreId,
                        principalTable: "ProductStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProductStores_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProductStores_ProductStoreId",
                table: "UserProductStores",
                column: "ProductStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductStores_UserId",
                table: "UserProductStores",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProductStores");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ProductStores");

            migrationBuilder.DropColumn(
                name: "ImageIconUrl",
                table: "ProductStores");

            migrationBuilder.AlterColumn<string>(
                name: "StoreCode",
                table: "ProductStores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
