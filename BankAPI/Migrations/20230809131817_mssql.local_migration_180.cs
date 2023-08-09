using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAPI.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_180 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
