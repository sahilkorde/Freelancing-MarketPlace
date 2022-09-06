using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial3.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    BidId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidPrice = table.Column<int>(type: "int", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.BidId);
                    table.ForeignKey(
                        name: "FK_Bids_AspNetUsers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bids_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "projectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_FreelancerId",
                table: "Bids",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ProjectId",
                table: "Bids",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");
        }
    }
}
