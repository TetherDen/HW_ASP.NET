using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_18_Student_Journal.Migrations
{
    /// <inheritdoc />
    public partial class GroupAndGroupMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_StudentId1",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_StudentId1",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "GroupMembers");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "GroupMembers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_StudentId",
                table: "GroupMembers",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_StudentId",
                table: "GroupMembers",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_StudentId",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_StudentId",
                table: "GroupMembers");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "GroupMembers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StudentId1",
                table: "GroupMembers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_StudentId1",
                table: "GroupMembers",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_StudentId1",
                table: "GroupMembers",
                column: "StudentId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
