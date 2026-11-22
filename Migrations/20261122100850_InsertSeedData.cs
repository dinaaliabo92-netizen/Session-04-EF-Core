using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Session_04_EF_Core.Migrations
{
    /// <inheritdoc />
    public partial class InsertSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Code", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Nasr City, Cairo", "Cairo Branch", "01012345678" },
                    { 2, "Smouha, Alexandria", "Alexandria Branch", "01112345678" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "BranchCode", "Email", "FullName", "HireDate", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "ahmed@bank.com", "Ahmed Mohamed", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01212345678" },
                    { 2, 2, "mona@bank.com", "Mona Ali", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01512345678" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: 2);
        }
    }
}
