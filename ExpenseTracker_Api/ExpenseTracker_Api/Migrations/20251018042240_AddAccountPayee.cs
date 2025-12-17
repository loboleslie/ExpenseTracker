using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExpenseTracker_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountPayee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PayeeName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountPayee",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    PayeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPayee", x => new { x.AccountId, x.PayeeId });
                    table.ForeignKey(
                        name: "FK_AccountPayee_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountPayee_Payee_PayeeId",
                        column: x => x.PayeeId,
                        principalTable: "Payee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPayee_PayeeId",
                table: "AccountPayee",
                column: "PayeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPayee");

            migrationBuilder.DropTable(
                name: "Payee");
        }
    }
}
