using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedTimeZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedTickets_AspNetUsers_UserId",
                table: "PurchasedTickets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PurchasedTickets",
                newName: "PurchaserId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedTickets_UserId",
                table: "PurchasedTickets",
                newName: "IX_PurchasedTickets_PurchaserId");

            migrationBuilder.AddColumn<string>(
                name: "DepartureTimeZone",
                table: "TimeTables",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartureTimeZone",
                table: "PurchasedTickets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedTickets_AspNetUsers_PurchaserId",
                table: "PurchasedTickets",
                column: "PurchaserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedTickets_AspNetUsers_PurchaserId",
                table: "PurchasedTickets");

            migrationBuilder.DropColumn(
                name: "DepartureTimeZone",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "DepartureTimeZone",
                table: "PurchasedTickets");

            migrationBuilder.RenameColumn(
                name: "PurchaserId",
                table: "PurchasedTickets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedTickets_PurchaserId",
                table: "PurchasedTickets",
                newName: "IX_PurchasedTickets_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedTickets_AspNetUsers_UserId",
                table: "PurchasedTickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
