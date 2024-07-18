using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hondrade_CodeFirstERD.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK_Applications_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CitizenID",
                table: "Applications",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ServiceID",
                table: "Applications",
                column: "ServiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
