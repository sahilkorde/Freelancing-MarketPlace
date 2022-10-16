using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial3.Migrations
{
    public partial class clientsetnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Projects_ProjectId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageBoxes_Projects_ProjectId",
                table: "MessageBoxes");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageBoxes_MessageBoxId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Projects_ProjectId",
                table: "Bids",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageBoxes_Projects_ProjectId",
                table: "MessageBoxes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageBoxes_MessageBoxId",
                table: "Messages",
                column: "MessageBoxId",
                principalTable: "MessageBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Projects_ProjectId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageBoxes_Projects_ProjectId",
                table: "MessageBoxes");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageBoxes_MessageBoxId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Projects_ProjectId",
                table: "Bids",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageBoxes_Projects_ProjectId",
                table: "MessageBoxes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageBoxes_MessageBoxId",
                table: "Messages",
                column: "MessageBoxId",
                principalTable: "MessageBoxes",
                principalColumn: "Id");
        }
    }
}
