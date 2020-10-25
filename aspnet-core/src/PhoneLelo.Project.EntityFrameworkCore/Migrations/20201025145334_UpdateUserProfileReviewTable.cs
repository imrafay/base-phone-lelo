using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneLelo.Project.Migrations
{
    public partial class UpdateUserProfileReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileReviews_AbpUsers_ReviewerId",
                table: "UserProfileReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileReviews_AbpUsers_UserId",
                table: "UserProfileReviews");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileReviews_UserId",
                table: "UserProfileReviews");

            migrationBuilder.DropColumn(
                name: "GuestEmailAddress",
                table: "UserProfileReviews");

            migrationBuilder.DropColumn(
                name: "GuestFirstName",
                table: "UserProfileReviews");

            migrationBuilder.DropColumn(
                name: "GuestLastName",
                table: "UserProfileReviews");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserProfileReviews",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ReviewerId",
                table: "UserProfileReviews",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserProfileReviewLikes",
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
                    UserProfileReviewId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileReviewLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileReviewLikes_UserProfileReviews_UserProfileReviewId",
                        column: x => x.UserProfileReviewId,
                        principalTable: "UserProfileReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileReviewLikes_UserProfileReviewId",
                table: "UserProfileReviewLikes",
                column: "UserProfileReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileReviews_AbpUsers_ReviewerId",
                table: "UserProfileReviews",
                column: "ReviewerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileReviews_AbpUsers_ReviewerId",
                table: "UserProfileReviews");

            migrationBuilder.DropTable(
                name: "UserProfileReviewLikes");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserProfileReviews",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ReviewerId",
                table: "UserProfileReviews",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "GuestEmailAddress",
                table: "UserProfileReviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuestFirstName",
                table: "UserProfileReviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuestLastName",
                table: "UserProfileReviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileReviews_UserId",
                table: "UserProfileReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileReviews_AbpUsers_ReviewerId",
                table: "UserProfileReviews",
                column: "ReviewerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileReviews_AbpUsers_UserId",
                table: "UserProfileReviews",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
