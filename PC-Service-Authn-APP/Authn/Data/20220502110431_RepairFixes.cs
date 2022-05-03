using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authn.Data
{
    public partial class RepairFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOwner",
                table: "Repair");

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "Repair",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userName",
                table: "Repair");

            migrationBuilder.AddColumn<int>(
                name: "IdOwner",
                table: "Repair",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
