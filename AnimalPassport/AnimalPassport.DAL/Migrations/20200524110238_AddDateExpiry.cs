using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalPassport.DataAccess.Migrations
{
    public partial class AddDateExpiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateExpiry",
                table: "MedicalOperation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExpiry",
                table: "MedicalOperation");
        }
    }
}
