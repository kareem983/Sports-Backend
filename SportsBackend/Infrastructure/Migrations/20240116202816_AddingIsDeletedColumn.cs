using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsDeletedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Tourments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Teams",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Players",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Matches",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Tourments");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Matches");
        }
    }
}
