using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIndexFromCurrencyRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CurrencyRate_TableId_CurrencyId",
                schema: "rates",
                table: "CurrencyRate");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_TableId",
                schema: "rates",
                table: "CurrencyRate",
                column: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CurrencyRate_TableId",
                schema: "rates",
                table: "CurrencyRate");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_TableId_CurrencyId",
                schema: "rates",
                table: "CurrencyRate",
                columns: new[] { "TableId", "CurrencyId" },
                unique: true);
        }
    }
}
