using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuzmaShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class PaymentMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_User_userId",
                table: "UserCart");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserCart",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCart_userId",
                table: "UserCart",
                newName: "IX_UserCart_UserId");

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_User_UserId",
                table: "UserCart",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_User_UserId",
                table: "UserCart");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserCart",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCart_UserId",
                table: "UserCart",
                newName: "IX_UserCart_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_User_userId",
                table: "UserCart",
                column: "userId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
