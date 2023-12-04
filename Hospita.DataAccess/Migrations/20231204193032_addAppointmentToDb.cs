using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.DataAccess.Migrations
{
    public partial class addAppointmentToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointmernts_AspNetUsers_ApllicationUserID",
                table: "Appointmernts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointmernts",
                table: "Appointmernts");

            migrationBuilder.RenameTable(
                name: "Appointmernts",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_Appointmernts_ApllicationUserID",
                table: "Appointments",
                newName: "IX_Appointments_ApllicationUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_ApllicationUserID",
                table: "Appointments",
                column: "ApllicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_ApllicationUserID",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointmernts");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ApllicationUserID",
                table: "Appointmernts",
                newName: "IX_Appointmernts_ApllicationUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointmernts",
                table: "Appointmernts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointmernts_AspNetUsers_ApllicationUserID",
                table: "Appointmernts",
                column: "ApllicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
