using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updated_ticket_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Coupons_CouponId",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "DiscountTypes",
                keyColumn: "Id",
                keyValue: new Guid("3b814b2a-78be-4258-86b7-5e28748512ea"));

            migrationBuilder.DeleteData(
                table: "DiscountTypes",
                keyColumn: "Id",
                keyValue: new Guid("50eaddfb-453e-4659-9eda-6db327866727"));

            migrationBuilder.DeleteData(
                table: "SeatTypes",
                keyColumn: "Id",
                keyValue: new Guid("4f86e1e1-0a7c-49d7-a1b9-f2b22c4b397d"));

            migrationBuilder.DeleteData(
                table: "SeatTypes",
                keyColumn: "Id",
                keyValue: new Guid("6751ee97-937c-495b-8fd1-21ee831abb51"));

            migrationBuilder.DeleteData(
                table: "SeatTypes",
                keyColumn: "Id",
                keyValue: new Guid("a3be1280-f68e-4cd0-9b76-e0e80ce3e171"));

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("09bb0827-2a26-4fbd-af34-038ba7ce854e"));

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("2d7923cd-18e5-4079-a923-fd6b4161669e"));

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("e5205282-f372-4cdd-b07f-8b13ad0423a8"));

            migrationBuilder.DeleteData(
                table: "TripTypes",
                keyColumn: "Id",
                keyValue: new Guid("90ef03ce-fa58-4e6d-abff-9fca12ae598a"));

            migrationBuilder.DeleteData(
                table: "TripTypes",
                keyColumn: "Id",
                keyValue: new Guid("edfcc57a-cf58-4e5f-bbf2-f8b4d501b4ce"));

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("194deab4-5950-4670-99c8-5d9f345f4ffc"), "Quantitative" },
                    { new Guid("a7420df9-eeed-47f0-b9d0-9c6363cd5cce"), "Percentage" }
                });

            migrationBuilder.InsertData(
                table: "SeatTypes",
                columns: new[] { "Id", "Name", "PriceMultiplier" },
                values: new object[,]
                {
                    { new Guid("37061534-0058-44a0-998d-6828a7921c3b"), "Business", 2.0m },
                    { new Guid("5d0993e0-e7c4-4c71-a8b2-55e9f26644c8"), "Comfort", 1.4m },
                    { new Guid("d8234193-2772-49af-afea-18e675bb2e44"), "Standard", 1.0m }
                });

            migrationBuilder.InsertData(
                table: "TicketStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1518e9d5-7a01-40ee-87ab-a23efbeb8c81"), "Booked" },
                    { new Guid("9368f0b7-9486-4f82-8cbc-b06aa70cc688"), "Completed" },
                    { new Guid("c92c0847-87e0-45fe-8ec8-c04a4edbe4aa"), "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "TripTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0634b2d0-4ef1-4ac8-936b-24c3e7f1e33d"), "Train" },
                    { new Guid("63bafa1d-c169-4238-9079-f1c531aa4e91"), "Airplane" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Coupons_CouponId",
                table: "Tickets",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Coupons_CouponId",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "DiscountTypes",
                keyColumn: "Id",
                keyValue: new Guid("194deab4-5950-4670-99c8-5d9f345f4ffc"));

            migrationBuilder.DeleteData(
                table: "DiscountTypes",
                keyColumn: "Id",
                keyValue: new Guid("a7420df9-eeed-47f0-b9d0-9c6363cd5cce"));

            migrationBuilder.DeleteData(
                table: "SeatTypes",
                keyColumn: "Id",
                keyValue: new Guid("37061534-0058-44a0-998d-6828a7921c3b"));

            migrationBuilder.DeleteData(
                table: "SeatTypes",
                keyColumn: "Id",
                keyValue: new Guid("5d0993e0-e7c4-4c71-a8b2-55e9f26644c8"));

            migrationBuilder.DeleteData(
                table: "SeatTypes",
                keyColumn: "Id",
                keyValue: new Guid("d8234193-2772-49af-afea-18e675bb2e44"));

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("1518e9d5-7a01-40ee-87ab-a23efbeb8c81"));

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("9368f0b7-9486-4f82-8cbc-b06aa70cc688"));

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("c92c0847-87e0-45fe-8ec8-c04a4edbe4aa"));

            migrationBuilder.DeleteData(
                table: "TripTypes",
                keyColumn: "Id",
                keyValue: new Guid("0634b2d0-4ef1-4ac8-936b-24c3e7f1e33d"));

            migrationBuilder.DeleteData(
                table: "TripTypes",
                keyColumn: "Id",
                keyValue: new Guid("63bafa1d-c169-4238-9079-f1c531aa4e91"));

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3b814b2a-78be-4258-86b7-5e28748512ea"), "Quantitative" },
                    { new Guid("50eaddfb-453e-4659-9eda-6db327866727"), "Percentage" }
                });

            migrationBuilder.InsertData(
                table: "SeatTypes",
                columns: new[] { "Id", "Name", "PriceMultiplier" },
                values: new object[,]
                {
                    { new Guid("4f86e1e1-0a7c-49d7-a1b9-f2b22c4b397d"), "Standard", 1.0m },
                    { new Guid("6751ee97-937c-495b-8fd1-21ee831abb51"), "Business", 2.0m },
                    { new Guid("a3be1280-f68e-4cd0-9b76-e0e80ce3e171"), "Comfort", 1.4m }
                });

            migrationBuilder.InsertData(
                table: "TicketStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("09bb0827-2a26-4fbd-af34-038ba7ce854e"), "Completed" },
                    { new Guid("2d7923cd-18e5-4079-a923-fd6b4161669e"), "Booked" },
                    { new Guid("e5205282-f372-4cdd-b07f-8b13ad0423a8"), "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "TripTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("90ef03ce-fa58-4e6d-abff-9fca12ae598a"), "Train" },
                    { new Guid("edfcc57a-cf58-4e5f-bbf2-f8b4d501b4ce"), "Airplane" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Coupons_CouponId",
                table: "Tickets",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
