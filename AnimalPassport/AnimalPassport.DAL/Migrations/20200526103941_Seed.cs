using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalPassport.DataAccess.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("001c1374-77ad-4c32-a0fd-3409d327322c"), "Власник домашньої тварини" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("3af641ec-b0c4-4eef-a939-d19b2e6adffb"), "Ветеринар" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("524e28e1-a8ff-4120-8bc8-a2773ee4247d"), "Член контрольних органів" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("001c1374-77ad-4c32-a0fd-3409d327322c"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("3af641ec-b0c4-4eef-a939-d19b2e6adffb"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("524e28e1-a8ff-4120-8bc8-a2773ee4247d"));
        }
    }
}
