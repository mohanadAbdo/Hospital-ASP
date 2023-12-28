﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.DataAccess.Migrations
{
    public partial class DeletedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
