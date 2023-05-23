using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class dbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userToProjects",
                table: "userToProjects");

            migrationBuilder.AddColumn<int>(
                name: "roleid",
                table: "userToProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_userToProjects",
                table: "userToProjects",
                columns: new[] { "UserId", "ProjectId", "roleid" });

            migrationBuilder.CreateIndex(
                name: "IX_userToProjects_roleid",
                table: "userToProjects",
                column: "roleid");

            migrationBuilder.AddForeignKey(
                name: "FK_userToProjects_roles_roleid",
                table: "userToProjects",
                column: "roleid",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userToProjects_roles_roleid",
                table: "userToProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userToProjects",
                table: "userToProjects");

            migrationBuilder.DropIndex(
                name: "IX_userToProjects_roleid",
                table: "userToProjects");

            migrationBuilder.DropColumn(
                name: "roleid",
                table: "userToProjects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "assignments");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_userToProjects",
                table: "userToProjects",
                columns: new[] { "UserId", "ProjectId" });
        }
    }
}
