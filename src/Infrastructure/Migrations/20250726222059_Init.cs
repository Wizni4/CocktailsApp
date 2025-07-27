using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClubPermissionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPermissionEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cocktail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CocktailIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CocktailIngredient_Cocktail_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CocktailIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientPricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientPricing_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailToIngredient",
                columns: table => new
                {
                    CocktailIngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailToIngredient", x => new { x.CocktailIngredientId, x.CocktailId });
                    table.ForeignKey(
                        name: "FK_CocktailToIngredient_CocktailIngredient_CocktailIngredientId",
                        column: x => x.CocktailIngredientId,
                        principalTable: "CocktailIngredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailToIngredient_Cocktail_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransaction_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Visibility = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Club_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClubCocktail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubCocktail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubCocktail_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubCocktail_Cocktail_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubMember_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubMember_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMemberToClubPermission",
                columns: table => new
                {
                    ClubMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMemberToClubPermission", x => new { x.ClubMemberId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ClubMemberToClubPermission_ClubMember_ClubMemberId",
                        column: x => x.ClubMemberId,
                        principalTable: "ClubMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubMemberToClubPermission_ClubPermissionEntity_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ClubPermissionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClubMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubRole_ClubMember_ClubMemberId",
                        column: x => x.ClubMemberId,
                        principalTable: "ClubMember",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClubRole_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Cocktail_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubRoleToClubPermission",
                columns: table => new
                {
                    ClubRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubRoleToClubPermission", x => new { x.ClubRoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ClubRoleToClubPermission_ClubPermissionEntity_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ClubPermissionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubRoleToClubPermission_ClubRole_ClubRoleId",
                        column: x => x.ClubRoleId,
                        principalTable: "ClubRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Club_AddressId",
                table: "Club",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Club_OwnerId",
                table: "Club",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubCocktail_ClubId",
                table: "ClubCocktail",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubCocktail_CocktailId",
                table: "ClubCocktail",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMember_ClubId",
                table: "ClubMember",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMember_UserId",
                table: "ClubMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMemberToClubPermission_PermissionId",
                table: "ClubMemberToClubPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRole_ClubId",
                table: "ClubRole",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRole_ClubMemberId",
                table: "ClubRole",
                column: "ClubMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRoleToClubPermission_PermissionId",
                table: "ClubRoleToClubPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredient_CocktailId",
                table: "CocktailIngredient",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredient_IngredientId",
                table: "CocktailIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailToIngredient_CocktailId",
                table: "CocktailToIngredient",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPricing_IngredientId",
                table: "IngredientPricing",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClubId",
                table: "Order",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_CocktailId",
                table: "OrderItem",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_IngredientId",
                table: "Stock",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_StockId",
                table: "StockTransaction",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_ClubMember_OwnerId",
                table: "Club",
                column: "OwnerId",
                principalTable: "ClubMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Address_AddressId",
                table: "Club");

            migrationBuilder.DropForeignKey(
                name: "FK_Club_ClubMember_OwnerId",
                table: "Club");

            migrationBuilder.DropTable(
                name: "ClubCocktail");

            migrationBuilder.DropTable(
                name: "ClubMemberToClubPermission");

            migrationBuilder.DropTable(
                name: "ClubRoleToClubPermission");

            migrationBuilder.DropTable(
                name: "CocktailToIngredient");

            migrationBuilder.DropTable(
                name: "IngredientPricing");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "StockTransaction");

            migrationBuilder.DropTable(
                name: "ClubPermissionEntity");

            migrationBuilder.DropTable(
                name: "ClubRole");

            migrationBuilder.DropTable(
                name: "CocktailIngredient");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Cocktail");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ClubMember");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
