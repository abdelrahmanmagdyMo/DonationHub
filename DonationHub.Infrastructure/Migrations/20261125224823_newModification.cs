using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAtUtc",
                table: "PaymentAttempts",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Campaigns");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Campaigns",
                type: "rowversion",
                rowVersion: true,
                nullable: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAtUtc",
                table: "Campaigns",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "PaymentAttempts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.DropColumn(
    name: "RowVersion",
    table: "Campaigns");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Campaigns",
                type: "varbinary(max)",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Campaigns",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);
        }
    }
}
