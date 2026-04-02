using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "Address", "City", "Postcode", "SchoolName", "State" },
                values: new object[,]
                {
                    { 1, "Trivandrum", "TVM", "695001", "Kerala Model School", "Kerala" },
                    { 2, "Kochi", "EKM", "682001", "St. Marys High School", "Kerala" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "ClassCode", "ClassName", "SchoolId", "TeacherEmail", "TeacherFirstName", "TeacherLastName" },
                values: new object[,]
                {
                    { 1, "10A-2026", "Class 10A", 1, "ramesh@test.com", "Ramesh", "Kumar" },
                    { 2, "10B-2026", "Class 10B", 1, "saritha@test.com", "Saritha", "Nair" },
                    { 3, "9A-2026", "Class 9A", 2, "manoj@test.com", "Manoj", "Pillai" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassId", "DateOfBirth", "Email", "FirstName", "Gender", "IsActive", "LastName", "Points" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2010, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "rahul@test.com", "Rahul", "Male", true, "Nair", 10 },
                    { 2, 1, new DateTime(2010, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "sneha@test.com", "Sneha", "Female", true, "V", 15 },
                    { 3, 2, new DateTime(2010, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "arjun@test.com", "Arjun", "Male", true, "K", 5 },
                    { 4, 3, new DateTime(2011, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "meera@test.com", "Meera", "Female", true, "S", 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
