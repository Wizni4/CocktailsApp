using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailsApp.Infrastructure.Migrations.EFReadDb
{
    /// <inheritdoc />
    public partial class FixIssue1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ClubRead");

            migrationBuilder.DropColumn(
                name: "Cocktails",
                table: "ClubRead");

            migrationBuilder.DropColumn(
                name: "Members",
                table: "ClubRead");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "ClubRead");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "ClubRead",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "ClubRead",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                table: "ClubRead",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "ClubRead",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "ClubRead",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_StreetNumber",
                table: "ClubRead",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ClubCocktailRead",
                columns: table => new
                {
                    ClubReadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubCocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImageId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubCocktailRead", x => new { x.ClubReadId, x.Id });
                    table.ForeignKey(
                        name: "FK_ClubCocktailRead_ClubRead_ClubReadId",
                        column: x => x.ClubReadId,
                        principalTable: "ClubRead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMemberRead",
                columns: table => new
                {
                    ClubReadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMemberRead", x => new { x.ClubReadId, x.Id });
                    table.ForeignKey(
                        name: "FK_ClubMemberRead_ClubRead_ClubReadId",
                        column: x => x.ClubReadId,
                        principalTable: "ClubRead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubRead_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubReadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubRead_Roles", x => new { x.ClubReadId, x.Id });
                    table.ForeignKey(
                        name: "FK_ClubRead_Roles_ClubRead_ClubReadId",
                        column: x => x.ClubReadId,
                        principalTable: "ClubRead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubCocktailIngredientRead",
                columns: table => new
                {
                    ClubCocktailReadClubReadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubCocktailReadId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    IsAlcoholic = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ImageId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Allergens = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubCocktailIngredientRead", x => new { x.ClubCocktailReadClubReadId, x.ClubCocktailReadId, x.Id });
                    table.ForeignKey(
                        name: "FK_ClubCocktailIngredientRead_ClubCocktailRead_ClubCocktailReadClubReadId_ClubCocktailReadId",
                        columns: x => new { x.ClubCocktailReadClubReadId, x.ClubCocktailReadId },
                        principalTable: "ClubCocktailRead",
                        principalColumns: new[] { "ClubReadId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMemberRead_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubMemberReadClubReadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubMemberReadId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMemberRead_Roles", x => new { x.ClubMemberReadClubReadId, x.ClubMemberReadId, x.Id });
                    table.ForeignKey(
                        name: "FK_ClubMemberRead_Roles_ClubMemberRead_ClubMemberReadClubReadId_ClubMemberReadId",
                        columns: x => new { x.ClubMemberReadClubReadId, x.ClubMemberReadId },
                        principalTable: "ClubMemberRead",
                        principalColumns: new[] { "ClubReadId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubCocktailIngredientRead");

            migrationBuilder.DropTable(
                name: "ClubMemberRead_Roles");

            migrationBuilder.DropTable(
                name: "ClubRead_Roles");

            migrationBuilder.DropTable(
                name: "ClubCocktailRead");

            migrationBuilder.DropTable(
                name: "ClubMemberRead");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "ClubRead");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "ClubRead");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                table: "ClubRead");

            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "ClubRead");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "ClubRead");

            migrationBuilder.DropColumn(
                name: "Address_StreetNumber",
                table: "ClubRead");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ClubRead",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.AddColumn<string>(
                name: "Cocktails",
                table: "ClubRead",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Members",
                table: "ClubRead",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "ClubRead",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
