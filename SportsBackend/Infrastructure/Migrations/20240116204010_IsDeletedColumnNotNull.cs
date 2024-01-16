using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsDeletedColumnNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Tourments",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Teams",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Players",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Matches",
                newName: "IsDeleted");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Tourments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Tourments",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Teams",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Players",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Matches",
                newName: "isDeleted");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Tourments",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Teams",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Players",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Matches",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
