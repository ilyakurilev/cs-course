using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reminder.Storage.SqlServer.Ef.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReminderContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChatId = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReminderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReminderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTimeOffset>(nullable: false),
                    Message = table.Column<string>(maxLength: 512, nullable: true),
                    ContactId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReminderItems_ReminderContacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "ReminderContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReminderItems_ReminderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ReminderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_ReminderContacts_ChatId",
                table: "ReminderContacts",
                column: "ChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReminderItems_ContactId",
                table: "ReminderItems",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ReminderItems_StatusId",
                table: "ReminderItems",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "UQ_ReminderStatuses_Status",
                table: "ReminderStatuses",
                column: "Status",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReminderItems");

            migrationBuilder.DropTable(
                name: "ReminderContacts");

            migrationBuilder.DropTable(
                name: "ReminderStatuses");
        }
    }
}
