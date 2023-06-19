using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSchadApp.Migrations
{
    /// <inheritdoc />
    public partial class updateTableInvoiceDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "InvoiceDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "InvoiceDetails");
        }
    }
}
