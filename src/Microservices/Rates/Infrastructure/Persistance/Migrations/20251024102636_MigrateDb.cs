using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rates");

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                schema: "rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SyncStrategy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cron = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                schema: "rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Source_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "rates",
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRate",
                schema: "rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRate_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "rates",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyRate_Table_TableId",
                        column: x => x.TableId,
                        principalSchema: "rates",
                        principalTable: "Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "rates",
                table: "Source",
                columns: new[] { "Id", "Cron", "Name", "Status", "SyncStrategy" },
                values: new object[] { new Guid("d07ebbb0-ee4b-4d13-8ef7-8ef007ae77e3"), "* * 16 3 * *", "Kursy średnie walut obcych – tabela B", 1, "NbpApiDateRange" });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_CurrencyId",
                schema: "rates",
                table: "CurrencyRate",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_TableId_CurrencyId",
                schema: "rates",
                table: "CurrencyRate",
                columns: new[] { "TableId", "CurrencyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Table_SourceId",
                schema: "rates",
                table: "Table",
                column: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRate",
                schema: "rates");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "rates");

            migrationBuilder.DropTable(
                name: "Table",
                schema: "rates");

            migrationBuilder.DropTable(
                name: "Source",
                schema: "rates");
        }
    }
}
