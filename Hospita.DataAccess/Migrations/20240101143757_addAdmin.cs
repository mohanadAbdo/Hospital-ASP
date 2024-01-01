using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.DataAccess.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Number", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Region", "SecurityStamp", "TheName", "TwoFactorEnabled", "UserName" },
                values: new object[] { "123", 0, "Sakarya", "ac6e9dc2-2018-4d48-83a7-d91e6a2c575c", "ApplicationUser", "B201210561@sakarya.edu.tr", true, false, null, null, "B201210561@sakarya.edu.tr", null, "AQAAAAEAACcQAAAAEBdI3OTOdNEyn8d2BsB+jc6Db7rE3XhjrUpxdabP5OCjXCDeIrBzuQ+oHkzTEr340A==", null, false, null, "2574c94a-6f04-49f3-99cd-1d44d4a7e205", "Admin", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "072b142a-c7ef-4503-b22b-984cc00461cf", "123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "072b142a-c7ef-4503-b22b-984cc00461cf", "123" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "123");
        }
    }
}
