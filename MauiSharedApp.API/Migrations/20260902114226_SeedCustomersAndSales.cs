using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MauiSharedApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomersAndSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "budi.santoso@example.com", "Budi Santoso", "081100000001" },
                    { 2, "siti.rahayu@example.com", "Siti Rahayu", "081100000002" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Latitude", "Longitude", "Name", "Phone", "SalesId" },
                values: new object[,]
                {
                    { 1, "Tugu Yogyakarta", null, -7.7830000000000004, 110.36709999999999, "Toko Tugu Jaya", null, 1 },
                    { 2, "Keraton Yogyakarta", null, -7.8052000000000001, 110.36409999999999, "Batik Keraton", null, 1 },
                    { 3, "Malioboro Street", null, -7.7925000000000004, 110.36539999999999, "Toko Oleh-Oleh Malioboro", null, 1 },
                    { 4, "Candi Prambanan", null, -7.7519999999999998, 110.4915, "Warung Prambanan", null, 2 },
                    { 5, "Candi Borobudur", null, -7.6078999999999999, 110.2038, "Kios Borobudur", null, 2 },
                    { 6, "Pantai Parangtritis", null, -8.0252999999999997, 110.33159999999999, "Cafe Parangtritis", null, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
