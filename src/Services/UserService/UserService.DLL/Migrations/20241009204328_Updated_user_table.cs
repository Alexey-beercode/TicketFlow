using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.DLL.Migrations
{
    /// <inheritdoc />
    public partial class Updated_user_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4196600d-b0b9-4175-8fc8-2481fb0f1b50"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("cdccbd43-84ea-4c1c-9966-012844bad823"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "Name" },
                values: new object[] { new Guid("4196600d-b0b9-4175-8fc8-2481fb0f1b50"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Resident" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("cdccbd43-84ea-4c1c-9966-012844bad823"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("583e1840-ba88-418d-ae9e-4ce7571f0946"), new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd65e7bd-e25a-4935-81d1-05093b5f48c0"),
                column: "PasswordHash",
                value: "$2a$11$ysIaNKoEeTwbREc8PUuuSuzhylORt5k8gG3ZoQdQDm8WbLohdR64u");
        }
    }
}
