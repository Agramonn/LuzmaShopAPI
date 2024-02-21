using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuzmaShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class ListofUserCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_UserCart_UserCartId",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_UserCartId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "UserCartId",
                table: "CartItem");

            migrationBuilder.AddColumn<int>(
                name: "UserCartId",
                table: "UserCartItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCartItem_UserCartId",
                table: "UserCartItem",
                column: "UserCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCartItem_UserCart_UserCartId",
                table: "UserCartItem",
                column: "UserCartId",
                principalTable: "UserCart",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCartItem_UserCart_UserCartId",
                table: "UserCartItem");

            migrationBuilder.DropIndex(
                name: "IX_UserCartItem_UserCartId",
                table: "UserCartItem");

            migrationBuilder.DropColumn(
                name: "UserCartId",
                table: "UserCartItem");

            migrationBuilder.AddColumn<int>(
                name: "UserCartId",
                table: "CartItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_UserCartId",
                table: "CartItem",
                column: "UserCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_UserCart_UserCartId",
                table: "CartItem",
                column: "UserCartId",
                principalTable: "UserCart",
                principalColumn: "Id");
        }
    }
}
