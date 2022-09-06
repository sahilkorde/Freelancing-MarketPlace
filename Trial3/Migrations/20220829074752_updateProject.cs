using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial3.Migrations
{
    public partial class updateProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "tags",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tags",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 10000);
        }
    }
}
