using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authn.Data
{
    public partial class RepairClassChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Repair",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Delivery",
                table: "Repair",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAvabilityTime",
                table: "Repair",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAvabilityTime",
                table: "Repair",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Repair",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Repair");

            migrationBuilder.DropColumn(
                name: "Delivery",
                table: "Repair");

            migrationBuilder.DropColumn(
                name: "EndAvabilityTime",
                table: "Repair");

            migrationBuilder.DropColumn(
                name: "StartAvabilityTime",
                table: "Repair");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Repair");
        }
    }
}
