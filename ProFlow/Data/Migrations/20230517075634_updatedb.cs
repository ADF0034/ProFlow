using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserToAssignments_AspNetUsers_UserId",
                table: "ApplicationUserToAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserToAssignments_assignments_AssignmentsId",
                table: "ApplicationUserToAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserToAssignments",
                table: "ApplicationUserToAssignments");

            migrationBuilder.RenameTable(
                name: "ApplicationUserToAssignments",
                newName: "UserToAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserToAssignments_AssignmentsId",
                table: "UserToAssignments",
                newName: "IX_UserToAssignments_AssignmentsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToAssignments",
                table: "UserToAssignments",
                columns: new[] { "UserId", "AssignmentsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserToAssignments_AspNetUsers_UserId",
                table: "UserToAssignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToAssignments_assignments_AssignmentsId",
                table: "UserToAssignments",
                column: "AssignmentsId",
                principalTable: "assignments",
                principalColumn: "AssigmentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToAssignments_AspNetUsers_UserId",
                table: "UserToAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToAssignments_assignments_AssignmentsId",
                table: "UserToAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToAssignments",
                table: "UserToAssignments");

            migrationBuilder.RenameTable(
                name: "UserToAssignments",
                newName: "ApplicationUserToAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_UserToAssignments_AssignmentsId",
                table: "ApplicationUserToAssignments",
                newName: "IX_ApplicationUserToAssignments_AssignmentsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserToAssignments",
                table: "ApplicationUserToAssignments",
                columns: new[] { "UserId", "AssignmentsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserToAssignments_AspNetUsers_UserId",
                table: "ApplicationUserToAssignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserToAssignments_assignments_AssignmentsId",
                table: "ApplicationUserToAssignments",
                column: "AssignmentsId",
                principalTable: "assignments",
                principalColumn: "AssigmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
