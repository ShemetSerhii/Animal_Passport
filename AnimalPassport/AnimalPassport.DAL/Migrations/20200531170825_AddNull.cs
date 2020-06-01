using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalPassport.DataAccess.Migrations
{
    public partial class AddNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<DateTime>(
                name: "DateExpiry",
                table: "MedicalOperation",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

      
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateExpiry",
                table: "MedicalOperation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

        }
    }
}
