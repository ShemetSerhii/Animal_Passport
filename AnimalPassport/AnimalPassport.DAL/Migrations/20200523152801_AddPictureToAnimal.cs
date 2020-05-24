using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalPassport.DataAccess.Migrations
{
    public partial class AddPictureToAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Animal",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Animal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Animal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Animal");
        }
    }
}
