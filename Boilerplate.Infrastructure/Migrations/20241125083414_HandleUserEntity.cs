using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations
{
    public partial class HandleUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_refresh_tokens",
                schema: "security");

            migrationBuilder.DropColumn(
                name: "address",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "birth_date",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "degree",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "is_system",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "is_verified",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "path",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "reset_code",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "reset_time",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "user_url",
                schema: "security",
                table: "Users");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "c1be5862-d402-4a31-b292-6aded859f7a8",
                columns: new[] { "created_at", "Email", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 25, 0, 53, 30, 0, DateTimeKind.Unspecified), "HealthTom@gmail.com", new DateTime(2024, 11, 25, 0, 53, 30, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                schema: "security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "birth_date",
                schema: "security",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "degree",
                schema: "security",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "is_system",
                schema: "security",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_verified",
                schema: "security",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "path",
                schema: "security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reset_code",
                schema: "security",
                table: "Users",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "reset_time",
                schema: "security",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_url",
                schema: "security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_refresh_tokens",
                schema: "security",
                columns: table => new
                {
                    refresh_token_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    agent = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expires_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ip_adress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    refresh_token = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    revoked_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_refresh_tokens", x => x.refresh_token_id);
                    table.ForeignKey(
                        name: "FK_user_refresh_tokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "c1be5862-d402-4a31-b292-6aded859f7a8",
                columns: new[] { "created_at", "Email", "updated_at" },
                values: new object[] { new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "amalassem21@gmail.com", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_user_refresh_tokens_UserId",
                schema: "security",
                table: "user_refresh_tokens",
                column: "UserId");
        }
    }
}
