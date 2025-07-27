using Microsoft.EntityFrameworkCore.Migrations;

using System;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_ClubMember_OwnerId",
                table: "Club");

            migrationBuilder.DropIndex(
                name: "IX_Club_OwnerId",
                table: "Club");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Club");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ClubMember",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubMember_OwnerId",
                table: "ClubMember",
                column: "OwnerId",
                unique: true,
                filter: "[OwnerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubMember_Club_OwnerId",
                table: "ClubMember",
                column: "OwnerId",
                principalTable: "Club",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubMember_Club_OwnerId",
                table: "ClubMember");

            migrationBuilder.DropIndex(
                name: "IX_ClubMember_OwnerId",
                table: "ClubMember");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ClubMember");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Club",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Club_OwnerId",
                table: "Club",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Club_ClubMember_OwnerId",
                table: "Club",
                column: "OwnerId",
                principalTable: "ClubMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
