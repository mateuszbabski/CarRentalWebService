using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitializeTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_RentalCompanies_RentalCompanyId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "RentalCompanyId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_RentalCompanies_RentalCompanyId",
                table: "Vehicles",
                column: "RentalCompanyId",
                principalTable: "RentalCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_RentalCompanies_RentalCompanyId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "RentalCompanyId",
                table: "Vehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_RentalCompanies_RentalCompanyId",
                table: "Vehicles",
                column: "RentalCompanyId",
                principalTable: "RentalCompanies",
                principalColumn: "Id");
        }
    }
}
