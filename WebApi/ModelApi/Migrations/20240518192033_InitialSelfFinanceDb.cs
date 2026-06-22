using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialSelfFinanceDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TYPES_EXPENSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPES_EXPENSES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TYPES_INCOMES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPES_INCOMES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EXPENSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMOUNT = table.Column<decimal>(type: "money", nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    TYPE_EXPENSE_ID = table.Column<int>(type: "integer", nullable: false),
                    COMMENTS = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXPENSES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EXPENSES_TYPES_EXPENSES_TYPE_EXPENSE_ID",
                        column: x => x.TYPE_EXPENSE_ID,
                        principalTable: "TYPES_EXPENSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INCOMES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMOUNT = table.Column<decimal>(type: "money", nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    TYPE_INCOME_ID = table.Column<int>(type: "integer", nullable: false),
                    COMMENTS = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCOMES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INCOMES_TYPES_INCOMES_TYPE_INCOME_ID",
                        column: x => x.TYPE_INCOME_ID,
                        principalTable: "TYPES_INCOMES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EXPENSES_TYPE_EXPENSE_ID",
                table: "EXPENSES",
                column: "TYPE_EXPENSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_INCOMES_TYPE_INCOME_ID",
                table: "INCOMES",
                column: "TYPE_INCOME_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EXPENSES");

            migrationBuilder.DropTable(
                name: "INCOMES");

            migrationBuilder.DropTable(
                name: "TYPES_EXPENSES");

            migrationBuilder.DropTable(
                name: "TYPES_INCOMES");
        }
    }
}
