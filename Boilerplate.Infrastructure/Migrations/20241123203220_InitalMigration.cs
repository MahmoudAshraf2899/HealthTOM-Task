using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_settings");

            migrationBuilder.DropTable(
                name: "department_translations");

            migrationBuilder.DropTable(
                name: "department_users");

            migrationBuilder.DropTable(
                name: "element_translations");

            migrationBuilder.DropTable(
                name: "files_library");

            migrationBuilder.DropTable(
                name: "language_translations");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "elements");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.CreateTable(
                name: "patient",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patient");

            migrationBuilder.CreateTable(
                name: "app_settings",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_system = table.Column<bool>(type: "bit", nullable: false),
                    key = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                    table.ForeignKey(
                        name: "FK_departments_departments_parent_id",
                        column: x => x.parent_id,
                        principalTable: "departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "elements",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    key = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_elements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "files_library",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    file_type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    directory = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files_library", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    is_system = table.Column<bool>(type: "bit", nullable: false),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "department_translations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_translations", x => x.id);
                    table.ForeignKey(
                        name: "FK_department_translations_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "department_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    is_manager = table.Column<bool>(type: "bit", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_department_users_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_department_users_Users_user_id",
                        column: x => x.user_id,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "element_translations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    element_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_element_translations", x => x.id);
                    table.ForeignKey(
                        name: "FK_element_translations_elements_element_id",
                        column: x => x.element_id,
                        principalTable: "elements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "language_translations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    language_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language_translations", x => x.id);
                    table.ForeignKey(
                        name: "FK_language_translations_languages_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "app_settings",
                columns: new[] { "id", "created_at", "created_by", "is_system", "key", "updated_at", "updated_by", "value" },
                values: new object[,]
                {
                    { 1L, null, null, true, "email_configuration", null, null, "{\"From\":\"digitalhub_dev@outlook.com\",\"SmtpServer\":\"smtp-mail.outlook.com\",\"Port\":\"587\",\"UserName\":\"digitalhub_dev@outlook.com\",\"Password\":\"DigitalDevTeam@123\"}" },
                    { 2L, null, null, true, "complete_migration_email", null, null, " تم رفع الملف ( {0} )بنجاح  " },
                    { 3L, null, null, true, "uncomplete_migration_email", null, null, " لم يتم رفع الملف  ( {0} ) لوجود اخطاء بالشيت ({1}) بالصف {2} والعمود {3}؛فلابد من مراجعة النشرة ثم قم بإعادة الرفع مره اخرى " }
                });

            migrationBuilder.InsertData(
                table: "languages",
                columns: new[] { "id", "created_at", "created_by", "direction", "flag", "is_default", "is_deleted", "is_system", "locale", "name", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "rtl", "eg", true, false, true, "ar", "العربية", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 2L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "ltr", "us", false, false, true, "en", "الإنجليزية", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" }
                });

            migrationBuilder.InsertData(
                table: "language_translations",
                columns: new[] { "id", "created_at", "created_by", "language_id", "locale", "name", "updated_at", "updated_by" },
                values: new object[] { 1L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", 1L, "en", "Arabic", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" });

            migrationBuilder.InsertData(
                table: "language_translations",
                columns: new[] { "id", "created_at", "created_by", "language_id", "locale", "name", "updated_at", "updated_by" },
                values: new object[] { 2L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", 2L, "en", "English", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" });

            migrationBuilder.CreateIndex(
                name: "key_unique",
                table: "app_settings",
                column: "key",
                unique: true,
                filter: "[key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_department_translations_department_id_locale",
                table: "department_translations",
                columns: new[] { "department_id", "locale" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_users_department_id",
                table: "department_users",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_users_user_id_department_id",
                table: "department_users",
                columns: new[] { "user_id", "department_id" },
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_departments_name",
                table: "departments",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_departments_parent_id",
                table: "departments",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_element_translations_element_id_locale",
                table: "element_translations",
                columns: new[] { "element_id", "locale" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_elements_key",
                table: "elements",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_language_translations_language_id_locale",
                table: "language_translations",
                columns: new[] { "language_id", "locale" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_languages_locale",
                table: "languages",
                column: "locale",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_languages_name",
                table: "languages",
                column: "name",
                unique: true);
        }
    }
}
