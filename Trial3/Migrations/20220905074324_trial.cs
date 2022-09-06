using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial3.Migrations
{
    public partial class trial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FreelancerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FreelancerId",
                table: "Projects",
                column: "FreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_FreelancerId",
                table: "Projects",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_FreelancerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_FreelancerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FreelancerId",
                table: "Projects");
        }
    }
}
