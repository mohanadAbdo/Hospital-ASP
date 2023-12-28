using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.DataAccess.Migrations
{
    public partial class AddedPaymentToHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "AppointmentHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDueDate",
                table: "AppointmentHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "AppointmentHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "AppointmentHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "AppointmentHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "AppointmentHeaders");

            migrationBuilder.DropColumn(
                name: "PaymentDueDate",
                table: "AppointmentHeaders");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "AppointmentHeaders");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "AppointmentHeaders");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "AppointmentHeaders");
        }
    }
}
