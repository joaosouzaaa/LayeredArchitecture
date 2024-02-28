using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FlowerShopManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flower_Shop",
                schema: "FlowerShop",
                table: "Flowers");

            migrationBuilder.DropIndex(
                name: "IX_Flowers_ShopId",
                schema: "FlowerShop",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "ShopId",
                schema: "FlowerShop",
                table: "Flowers");

            migrationBuilder.CreateTable(
                name: "ShopsFlowers",
                schema: "FlowerShop",
                columns: table => new
                {
                    FlowersId = table.Column<int>(type: "int", nullable: false),
                    ShopsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopsFlowers", x => new { x.FlowersId, x.ShopsId });
                    table.ForeignKey(
                        name: "FK_ShopsFlowers_Flowers_FlowersId",
                        column: x => x.FlowersId,
                        principalSchema: "FlowerShop",
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopsFlowers_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalSchema: "FlowerShop",
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopsFlowers_ShopsId",
                schema: "FlowerShop",
                table: "ShopsFlowers",
                column: "ShopsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopsFlowers",
                schema: "FlowerShop");

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                schema: "FlowerShop",
                table: "Flowers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_ShopId",
                schema: "FlowerShop",
                table: "Flowers",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flower_Shop",
                schema: "FlowerShop",
                table: "Flowers",
                column: "ShopId",
                principalSchema: "FlowerShop",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
