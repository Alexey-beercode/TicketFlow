using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.DLL.Migrations
{
    /// <inheritdoc />
    public partial class Update_user_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9a9b46de-6051-4f6e-99a6-6200eec764fa"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("7743987c-6ab5-495a-889d-2666b272fb78"));

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "Name" },
                values: new object[] { new Guid("844f07cf-707a-475b-81d3-18d529a46d4c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Resident" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("715d6fe3-b093-4b03-9a37-aa914a1e5ad1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("583e1840-ba88-418d-ae9e-4ce7571f0946"), new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                column: "PasswordHash",
                value: "$2a$11$lFxiohVcdqf6w1haIKP2Me.Ycws5HgQLMsal12VnZS4EbL7TF1jHG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("844f07cf-707a-475b-81d3-18d529a46d4c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("715d6fe3-b093-4b03-9a37-aa914a1e5ad1"));

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "Name" },
                values: new object[] { new Guid("9a9b46de-6051-4f6e-99a6-6200eec764fa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Resident" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("7743987c-6ab5-495a-889d-2666b272fb78"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("583e1840-ba88-418d-ae9e-4ce7571f0946"), new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                column: "PasswordHash",
                value: "$2a$11$RcaRfcYtmzTiSSwGvr4WPO5IoUp1S2fB5HaZ39V5r0mYt.tWEUSqC");
        }
    }
}
