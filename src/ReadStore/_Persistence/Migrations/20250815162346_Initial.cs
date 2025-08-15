using Microsoft.EntityFrameworkCore.Migrations;

using System;

#nullable disable

namespace CocktailsApp.ReadStore._Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubRead",
                columns: table => new
                {
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Visibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StreetNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    State = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ImageId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubRead", x => x.ClubId);
                });

            migrationBuilder.CreateTable(
                name: "CocktailIngredientRead",
                columns: table => new
                {
                    CocktailIngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IngredientType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsAlcoholic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailIngredientRead", x => x.CocktailIngredientId);
                });

            migrationBuilder.CreateTable(
                name: "CocktailRead",
                columns: table => new
                {
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImageId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailRead", x => x.CocktailId);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRead",
                columns: table => new
                {
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IngredientType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsAlcoholic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRead", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "UserSummaryRead",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSummaryRead", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ClubRoleRead",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsOwnerRole = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubRoleRead", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_ClubRoleRead_ClubRead_ClubId",
                        column: x => x.ClubId,
                        principalTable: "ClubRead",
                        principalColumn: "ClubId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubCocktailRead",
                columns: table => new
                {
                    ClubCocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubCocktailRead", x => x.ClubCocktailId);
                    table.ForeignKey(
                        name: "FK_ClubCocktailRead_ClubRead_ClubId",
                        column: x => x.ClubId,
                        principalTable: "ClubRead",
                        principalColumn: "ClubId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubCocktailRead_CocktailRead_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "CocktailRead",
                        principalColumn: "CocktailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllergenRead",
                columns: table => new
                {
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergenRead", x => new { x.IngredientId, x.Name });
                    table.ForeignKey(
                        name: "FK_AllergenRead_IngredientRead_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "IngredientRead",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMemberRead",
                columns: table => new
                {
                    ClubMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMemberRead", x => x.ClubMemberId);
                    table.ForeignKey(
                        name: "FK_ClubMemberRead_ClubRead_ClubId",
                        column: x => x.ClubId,
                        principalTable: "ClubRead",
                        principalColumn: "ClubId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubMemberRead_UserSummaryRead_UserId",
                        column: x => x.UserId,
                        principalTable: "UserSummaryRead",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubRolePermissionRead",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubRolePermissionRead", x => new { x.RoleId, x.Permission });
                    table.ForeignKey(
                        name: "FK_ClubRolePermissionRead_ClubRead_ClubId",
                        column: x => x.ClubId,
                        principalTable: "ClubRead",
                        principalColumn: "ClubId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubRolePermissionRead_ClubRoleRead_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ClubRoleRead",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMemberRoleRead",
                columns: table => new
                {
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsOwnerRole = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMemberRoleRead", x => new { x.ClubId, x.ClubMemberId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ClubMemberRoleRead_ClubMemberRead_ClubMemberId",
                        column: x => x.ClubMemberId,
                        principalTable: "ClubMemberRead",
                        principalColumn: "ClubMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubMemberRoleRead_ClubRead_ClubId",
                        column: x => x.ClubId,
                        principalTable: "ClubRead",
                        principalColumn: "ClubId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubMemberRoleRead_ClubRoleRead_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ClubRoleRead",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergenRead_IngredientId",
                table: "AllergenRead",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubCocktailRead_ClubCocktailId",
                table: "ClubCocktailRead",
                column: "ClubCocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubCocktailRead_ClubId",
                table: "ClubCocktailRead",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubCocktailRead_CocktailId",
                table: "ClubCocktailRead",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMemberRead_ClubId",
                table: "ClubMemberRead",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMemberRead_ClubMemberId",
                table: "ClubMemberRead",
                column: "ClubMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMemberRead_UserId",
                table: "ClubMemberRead",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMemberRoleRead_ClubId",
                table: "ClubMemberRoleRead",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMemberRoleRead_ClubMemberId",
                table: "ClubMemberRoleRead",
                column: "ClubMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMemberRoleRead_RoleId",
                table: "ClubMemberRoleRead",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRead_ClubId",
                table: "ClubRead",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRolePermissionRead_ClubId",
                table: "ClubRolePermissionRead",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRolePermissionRead_RoleId",
                table: "ClubRolePermissionRead",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRoleRead_ClubId",
                table: "ClubRoleRead",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRoleRead_RoleId",
                table: "ClubRoleRead",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredientRead_CocktailId",
                table: "CocktailIngredientRead",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredientRead_CocktailIngredientId",
                table: "CocktailIngredientRead",
                column: "CocktailIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredientRead_IngredientId",
                table: "CocktailIngredientRead",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailRead_CocktailId",
                table: "CocktailRead",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRead_IngredientId",
                table: "IngredientRead",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSummaryRead_UserId",
                table: "UserSummaryRead",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergenRead");

            migrationBuilder.DropTable(
                name: "ClubCocktailRead");

            migrationBuilder.DropTable(
                name: "ClubMemberRoleRead");

            migrationBuilder.DropTable(
                name: "ClubRolePermissionRead");

            migrationBuilder.DropTable(
                name: "CocktailIngredientRead");

            migrationBuilder.DropTable(
                name: "IngredientRead");

            migrationBuilder.DropTable(
                name: "CocktailRead");

            migrationBuilder.DropTable(
                name: "ClubMemberRead");

            migrationBuilder.DropTable(
                name: "ClubRoleRead");

            migrationBuilder.DropTable(
                name: "UserSummaryRead");

            migrationBuilder.DropTable(
                name: "ClubRead");
        }
    }
}
