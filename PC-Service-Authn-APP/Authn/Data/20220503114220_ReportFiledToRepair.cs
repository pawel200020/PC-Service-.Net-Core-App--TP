using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authn.Data
{
    public partial class ReportFiledToRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Report",
                table: "Repair",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Report",
                table: "Repair");
        }
    }
}
