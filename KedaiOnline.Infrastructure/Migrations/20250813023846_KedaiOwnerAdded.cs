using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KedaiOnline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KedaiOwnerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "KedaiOnline",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE KedaiOnline SET OwnerId = (SELECT TOP 1 Id FROM AspNetUsers)");

            migrationBuilder.CreateIndex(
                name: "IX_KedaiOnline_OwnerId",
                table: "KedaiOnline",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KedaiOnline_AspNetUsers_OwnerId",
                table: "KedaiOnline",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KedaiOnline_AspNetUsers_OwnerId",
                table: "KedaiOnline");

            migrationBuilder.DropIndex(
                name: "IX_KedaiOnline_OwnerId",
                table: "KedaiOnline");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "KedaiOnline");
        }
    }
}
