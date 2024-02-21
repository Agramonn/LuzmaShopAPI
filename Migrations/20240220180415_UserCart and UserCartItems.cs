using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuzmaShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserCartandUserCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCartId",
                table: "CartItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    Ordered = table.Column<bool>(type: "bit", nullable: false),
                    OrderedOn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCart_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_UserCartId",
                table: "CartItem",
                column: "UserCartId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCart_userId",
                table: "UserCart",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCartItem_ProductId",
                table: "UserCartItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_UserCart_UserCartId",
                table: "CartItem",
                column: "UserCartId",
                principalTable: "UserCart",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_UserCart_UserCartId",
                table: "CartItem");

            migrationBuilder.DropTable(
                name: "UserCart");

            migrationBuilder.DropTable(
                name: "UserCartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_UserCartId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "UserCartId",
                table: "CartItem");
        }
    }
}
