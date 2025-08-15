using Microsoft.EntityFrameworkCore.Migrations;

using System;

#nullable disable

namespace CocktailsApp.ReadStore._Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedKafka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessedEvent",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Partition = table.Column<int>(type: "int", nullable: false),
                    Offset = table.Column<long>(type: "bigint", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessedEvent", x => x.EventId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessedEvent");
        }
    }
}
