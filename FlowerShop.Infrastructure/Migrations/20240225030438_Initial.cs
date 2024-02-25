using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FlowerShop");

            migrationBuilder.CreateTable(
                name: "Shops",
                schema: "FlowerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    location = table.Column<string>(type: "varchar(200)", nullable: false),
                    email = table.Column<string>(type: "varchar(150)", nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flowers",
                schema: "FlowerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    color = table.Column<string>(type: "varchar(100)", nullable: false),
                    species = table.Column<string>(type: "varchar(100)", nullable: false),
                    blooming_season = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flower_Shop",
                        column: x => x.ShopId,
                        principalSchema: "FlowerShop",
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_ShopId",
                schema: "FlowerShop",
                table: "Flowers",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flowers",
                schema: "FlowerShop");

            migrationBuilder.DropTable(
                name: "Shops",
                schema: "FlowerShop");
        }
    }
}
