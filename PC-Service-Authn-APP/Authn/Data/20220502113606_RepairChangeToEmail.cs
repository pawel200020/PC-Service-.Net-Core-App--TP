using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authn.Data
{
    public partial class RepairChangeToEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Repair",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Repair",
                newName: "userName");
        }
    }
}
