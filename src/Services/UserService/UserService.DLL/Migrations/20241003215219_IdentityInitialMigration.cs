using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.DLL.Migrations
{
    /// <inheritdoc />
    public partial class IdentityInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7daee94c-e18b-4d6b-9eb9-0e03aba4ea2d"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedAt", "Name", "NormalizedName" },
                values: new object[] { new Guid("ec47316d-dd79-4764-844c-8d85c9b3eb03"), null, null, "Resident", "RESIDENT" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d636c8c0-1bee-4e86-bbec-3c458a37590c", "AQAAAAIAAYagAAAAEONcxqLzmwbwvbpJQQ5CAAYdtmgcIbsadwnxzwgIjWagw3aBk+8IhHrBiLeICSezZw==", "44a1de71-b4ec-46a2-976c-6f8204d95e07" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ec47316d-dd79-4764-844c-8d85c9b3eb03"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedAt", "Name", "NormalizedName" },
                values: new object[] { new Guid("7daee94c-e18b-4d6b-9eb9-0e03aba4ea2d"), null, null, "Resident", "RESIDENT" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60b10986-3401-4d71-a324-0d75cf7b3a78", "AQAAAAIAAYagAAAAEJcs7E3Qt2aOw4ECSg+jaly/UIKGTbRth7hoNEeyb/hL5gw6G0HwQeQBLogR2MPA6g==", "31f8340d-ac60-4998-829b-0634eb6eaf05" });
        }
    }
}
