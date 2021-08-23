using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reminder.Storage.SqlServer.Ef.Migrations
{
    public partial class InsertStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var statuses = (ReminderItemStatus[]) Enum.GetValues(typeof(ReminderItemStatus));
            var values = new object[statuses.Length, 2];
            for (var i = 0; i < statuses.Length; i++)
            {
                values[i, 0] = (int) statuses[i];
                values[i, 1] = statuses[i].ToString();
            }
            migrationBuilder.InsertData(
                "ReminderStatuses",
                new[] {"Id", "Status"},
                values
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var values = Enum.GetValues(typeof(ReminderItemStatus))
                .Cast<ReminderItemStatus>()
                .Select(status => (object) (int) status)
                .ToArray();

            migrationBuilder.DeleteData("ReminderStatuses", "Id", values);
        }
    }
}
