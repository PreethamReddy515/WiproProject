using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeChatApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class LatestUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GroupMembers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserId",
                table: "GroupMembers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_UserId",
                table: "GroupMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_UserId",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_UserId",
                table: "GroupMembers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GroupMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
