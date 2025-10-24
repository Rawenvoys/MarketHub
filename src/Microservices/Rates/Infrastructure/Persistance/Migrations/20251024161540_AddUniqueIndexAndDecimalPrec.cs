using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexAndDecimalPrec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Mid",
                schema: "rates",
                table: "CurrencyRate",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Code_Name",
                schema: "rates",
                table: "Currency",
                columns: new[] { "Code", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Currency_Code_Name",
                schema: "rates",
                table: "Currency");

            migrationBuilder.AlterColumn<decimal>(
                name: "Mid",
                schema: "rates",
                table: "CurrencyRate",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");
        }
    }
}
