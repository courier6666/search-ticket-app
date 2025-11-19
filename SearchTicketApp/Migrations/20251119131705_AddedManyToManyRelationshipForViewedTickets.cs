using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedManyToManyRelationshipForViewedTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_UserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "OnSaleTicketUser",
                columns: table => new
                {
                    UserViewedId = table.Column<int>(type: "INTEGER", nullable: false),
                    ViewedTicketsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnSaleTicketUser", x => new { x.UserViewedId, x.ViewedTicketsId });
                    table.ForeignKey(
                        name: "FK_OnSaleTicketUser_AspNetUsers_UserViewedId",
                        column: x => x.UserViewedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnSaleTicketUser_Tickets_ViewedTicketsId",
                        column: x => x.ViewedTicketsId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnSaleTicketUser_ViewedTicketsId",
                table: "OnSaleTicketUser",
                column: "ViewedTicketsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnSaleTicketUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tickets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
