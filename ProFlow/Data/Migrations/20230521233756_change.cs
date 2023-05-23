using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "assignments");

            migrationBuilder.AddColumn<bool>(
                name: "Assignt",
                table: "assignments",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assignt",
                table: "assignments");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
