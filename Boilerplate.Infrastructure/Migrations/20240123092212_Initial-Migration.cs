using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "app_settings",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    key = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_system = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
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
                    key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
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
                    file = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    directory = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    file_type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
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
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    is_system = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "migrations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    no_sheets = table.Column<int>(type: "int", nullable: false),
                    file_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    migration_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    error_sheet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    error_row = table.Column<int>(type: "int", nullable: false),
                    error_column = table.Column<int>(type: "int", nullable: false),
                    error = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_migrations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission_modules",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_system = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission_modules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: true),
                    is_system = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_system = table.Column<bool>(type: "bit", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    degree = table.Column<int>(type: "int", nullable: false),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_banned = table.Column<bool>(type: "bit", nullable: false),
                    reset_code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    reset_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_verified = table.Column<bool>(type: "bit", nullable: false),
                    user_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "department_translations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
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
                name: "element_translations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    element_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
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
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    language_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "role_permission",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    module_id = table.Column<long>(type: "bigint", nullable: false),
                    operation_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_permission_permission_modules_module_id",
                        column: x => x.module_id,
                        principalTable: "permission_modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_role_permission_Roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_translations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    locale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_translations", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_translations_Roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "department_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    is_manager = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
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
                name: "user_refresh_tokens",
                schema: "security",
                columns: table => new
                {
                    refresh_token_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    refresh_token = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ip_adress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    agent = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    expires_on = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "user_validation_tokens",
                schema: "security",
                columns: table => new
                {
                    token_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    token = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ip_adress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    agent = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    expires_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    revoked_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_validation_tokens", x => x.token_id);
                    table.ForeignKey(
                        name: "FK_user_validation_tokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId1",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "code", "ConcurrencyStamp", "created_at", "created_by", "Discriminator", "is_deleted", "is_system", "Name", "NormalizedName", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { "61580090-2de4-4f3a-8d93-34e32fc48ecb", null, "d65c8300-f921-4eb4-9241-98ddc63c50f5", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "Role", false, true, "Editor", "EDITOR", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { "67df368b-c097-4020-b6b9-f2c03aaae7a8", null, "1d548e79-f43f-4452-af54-88327d81a3a4", new DateTime(2023, 9, 27, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "Role", false, true, "Admin", "ADMIN", new DateTime(2023, 9, 27, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { "b17cc416-89b4-455d-a58a-4b4e8503e995", null, "6acc130c-f695-4134-9e9c-19ba25872d52", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "Role", false, true, "Super Admin", "SUPER ADMIN", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { "fd44e5d3-57ed-4276-98b6-844f06045062", null, "d0926134-d4c9-4ce4-9802-3f1c6605a33b", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "Role", false, true, "Content Creator", "CONTENT CREATOR", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "address", "birth_date", "ConcurrencyStamp", "created_at", "created_by", "degree", "Email", "EmailConfirmed", "first_name", "gender", "is_banned", "is_system", "is_verified", "last_name", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "path", "phone_number", "PhoneNumberConfirmed", "reset_code", "reset_time", "SecurityStamp", "TwoFactorEnabled", "updated_at", "updated_by", "UserName", "user_url" },
                values: new object[] { "c1be5862-d402-4a31-b292-6aded859f7a8", 0, null, null, "e3b9c8c2-31c8-4899-99c4-6ebc51e2542b", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", 0, "amalassem21@gmail.com", true, "Admin", 1, false, false, false, "Admin", true, null, "AMALASSEM21@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAECi3lT6l+1HKEawLjldUyT9Azn1jpzQZeMkapDbycmjUT++jy2voAG5zGBg4Spj+iA==", null, "01145907543", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SLKWB2MKZHIHR3TK4VHZYLZSAMIKAUCF", false, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "Admin", null });

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
                table: "permission_modules",
                columns: new[] { "id", "created_at", "created_by", "is_deleted", "is_system", "name", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, true, "Departments", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 2L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, true, "Users", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 3L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, true, "Roles", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 4L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, true, "Permissions", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 5L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, true, "Settings", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 6L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, true, "Languages", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 7L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, true, "Department Users", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 9L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, true, "Dashboard", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "created_at", "created_by", "Discriminator", "id", "updated_at", "updated_by", "UserId1" },
                values: new object[,]
                {
                    { "61580090-2de4-4f3a-8d93-34e32fc48ecb", "c1be5862-d402-4a31-b292-6aded859f7a8", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "UserRole", 0L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", null },
                    { "b17cc416-89b4-455d-a58a-4b4e8503e995", "c1be5862-d402-4a31-b292-6aded859f7a8", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "UserRole", 0L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", null },
                    { "fd44e5d3-57ed-4276-98b6-844f06045062", "c1be5862-d402-4a31-b292-6aded859f7a8", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", "UserRole", 0L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", null }
                });

            migrationBuilder.InsertData(
                table: "language_translations",
                columns: new[] { "id", "created_at", "created_by", "language_id", "locale", "name", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", 1L, "en", "Arabic", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 2L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", 2L, "en", "English", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" }
                });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "id", "created_at", "created_by", "is_deleted", "module_id", "operation_id", "role_id", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 1L, 1L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 2L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 1L, 2L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 3L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 1L, 3L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 4L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 1L, 4L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 5L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 1L, 5L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 6L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 2L, 1L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 7L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 2L, 2L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 8L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 2L, 3L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 9L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 2L, 4L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 10L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 2L, 5L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 11L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 3L, 1L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 12L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 3L, 2L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 13L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 3L, 3L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 14L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 3L, 4L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 15L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 3L, 5L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 16L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 4L, 1L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 17L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 4L, 2L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 18L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 4L, 3L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 19L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 4L, 4L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 20L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 4L, 5L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 21L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 5L, 1L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 22L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 5L, 2L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 23L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 5L, 3L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 24L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 5L, 4L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 25L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 5L, 5L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 26L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 6L, 1L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 27L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 6L, 2L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 28L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 6L, 3L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 29L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 6L, 4L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 30L, new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 6L, 5L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2021, 11, 15, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 31L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 7L, 1L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 32L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 7L, 2L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 33L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 7L, 3L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 34L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 7L, 4L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 35L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 7L, 5L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 36L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 9L, 1L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" },
                    { 37L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 9L, 2L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" }
                });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "id", "created_at", "created_by", "is_deleted", "module_id", "operation_id", "role_id", "updated_at", "updated_by" },
                values: new object[] { 38L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 9L, 3L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "id", "created_at", "created_by", "is_deleted", "module_id", "operation_id", "role_id", "updated_at", "updated_by" },
                values: new object[] { 39L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 9L, 4L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "id", "created_at", "created_by", "is_deleted", "module_id", "operation_id", "role_id", "updated_at", "updated_by" },
                values: new object[] { 40L, new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8", false, 9L, 5L, "b17cc416-89b4-455d-a58a-4b4e8503e995", new DateTime(2023, 2, 20, 0, 53, 30, 0, DateTimeKind.Unspecified), "c1be5862-d402-4a31-b292-6aded859f7a8" });

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

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_module_id",
                table: "role_permission",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_role_id",
                table: "role_permission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_translations_role_id_locale",
                table: "role_translations",
                columns: new[] { "role_id", "locale" },
                unique: true,
                filter: "[role_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "security",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "security",
                table: "Roles",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "security",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_user_refresh_tokens_UserId",
                schema: "security",
                table: "user_refresh_tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_validation_tokens_UserId",
                schema: "security",
                table: "user_validation_tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "security",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "security",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "security",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                schema: "security",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId1",
                schema: "security",
                table: "UserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "security",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName_Email",
                schema: "security",
                table: "Users",
                columns: new[] { "UserName", "Email" },
                unique: true,
                filter: "[UserName] IS NOT NULL AND [Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "security",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "migrations");

            migrationBuilder.DropTable(
                name: "role_permission");

            migrationBuilder.DropTable(
                name: "role_translations");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "security");

            migrationBuilder.DropTable(
                name: "user_refresh_tokens",
                schema: "security");

            migrationBuilder.DropTable(
                name: "user_validation_tokens",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "security");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "elements");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "permission_modules");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "security");
        }
    }
}
