using Microsoft.EntityFrameworkCore.Migrations;

namespace NugetAuditor.Core.Migrations
{
    public partial class addedcopyright : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Copyright",
                table: "Results",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidCopyright",
                table: "Results",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copyright",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "IsValidCopyright",
                table: "Results");
        }
    }
}
