using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial3.Migrations
{
    public partial class ProjectMessageBoxRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageBoxes_Projects_ProjectId",
                table: "MessageBoxes");

            migrationBuilder.DropIndex(
                name: "IX_MessageBoxes_ProjectId",
                table: "MessageBoxes");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoxes_ProjectId",
                table: "MessageBoxes",
                column: "ProjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageBoxes_Projects_ProjectId",
                table: "MessageBoxes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "projectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageBoxes_Projects_ProjectId",
                table: "MessageBoxes");

            migrationBuilder.DropIndex(
                name: "IX_MessageBoxes_ProjectId",
                table: "MessageBoxes");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoxes_ProjectId",
                table: "MessageBoxes",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageBoxes_Projects_ProjectId",
                table: "MessageBoxes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
