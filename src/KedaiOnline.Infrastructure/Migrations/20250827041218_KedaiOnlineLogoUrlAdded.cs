using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KedaiOnline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KedaiOnlineLogoUrlAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "KedaiOnline",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "KedaiOnline");
        }
    }
}
