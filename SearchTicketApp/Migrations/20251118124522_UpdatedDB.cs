using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchTicketApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TimeTables_TicketId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "PurchasedTickets");

            migrationBuilder.RenameColumn(
                name: "DepartureTimeZone",
                table: "PurchasedTickets",
                newName: "DepartureTimeUtc");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "PurchasedTickets",
                newName: "ArrivalTimeUtc");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTimeUtc",
                table: "Tickets",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DepartureLocalTimeZone",
                table: "Tickets",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTimeUtc",
                table: "Tickets",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DepartureLocalTimeZone",
                table: "PurchasedTickets",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OnSaleTicketId",
                table: "PurchasedTickets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_TicketId",
                table: "TimeTables",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedTickets_OnSaleTicketId",
                table: "PurchasedTickets",
                column: "OnSaleTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedTickets_Tickets_OnSaleTicketId",
                table: "PurchasedTickets",
                column: "OnSaleTicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedTickets_Tickets_OnSaleTicketId",
                table: "PurchasedTickets");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_TicketId",
                table: "TimeTables");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedTickets_OnSaleTicketId",
                table: "PurchasedTickets");

            migrationBuilder.DropColumn(
                name: "ArrivalTimeUtc",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DepartureLocalTimeZone",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DepartureTimeUtc",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DepartureLocalTimeZone",
                table: "PurchasedTickets");

            migrationBuilder.DropColumn(
                name: "OnSaleTicketId",
                table: "PurchasedTickets");

            migrationBuilder.RenameColumn(
                name: "DepartureTimeUtc",
                table: "PurchasedTickets",
                newName: "DepartureTimeZone");

            migrationBuilder.RenameColumn(
                name: "ArrivalTimeUtc",
                table: "PurchasedTickets",
                newName: "DepartureTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "PurchasedTickets",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_TicketId",
                table: "TimeTables",
                column: "TicketId",
                unique: true);
        }
    }
}
