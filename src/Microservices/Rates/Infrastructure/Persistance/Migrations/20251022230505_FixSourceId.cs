using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixSourceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "rates",
                table: "Source",
                keyColumn: "Id",
                keyValue: new Guid("3f503a06-575c-4c2f-b5a8-ed86753419e9"));

            migrationBuilder.InsertData(
                schema: "rates",
                table: "Source",
                columns: new[] { "Id", "CronExpression", "Name", "Status", "SyncStrategy" },
                values: new object[] { new Guid("d07ebbb0-ee4b-4d13-8ef7-8ef007ae77e3"), "* * 16 3 * *", "Kursy średnie walut obcych – tabela B", 1, "NbpApiDateRange" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "rates",
                table: "Source",
                keyColumn: "Id",
                keyValue: new Guid("d07ebbb0-ee4b-4d13-8ef7-8ef007ae77e3"));

            migrationBuilder.InsertData(
                schema: "rates",
                table: "Source",
                columns: new[] { "Id", "CronExpression", "Name", "Status", "SyncStrategy" },
                values: new object[] { new Guid("3f503a06-575c-4c2f-b5a8-ed86753419e9"), "* * 16 3 * *", "Kursy średnie walut obcych – tabela B", 1, "NbpApiDateRange" });
        }
    }
}
