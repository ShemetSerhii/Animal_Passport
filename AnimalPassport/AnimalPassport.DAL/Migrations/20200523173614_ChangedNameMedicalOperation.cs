using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalPassport.DataAccess.Migrations
{
    public partial class ChangedNameMedicalOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "MedicalOperation");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MedicalOperation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MedicalOperation");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MedicalOperation",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
