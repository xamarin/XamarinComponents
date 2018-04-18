using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NugetAuditor.Core.Migrations
{
    public partial class InitialDbLayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    PackageId = table.Column<string>(nullable: false),
                    CurrentVersion = table.Column<string>(nullable: true),
                    IconUrlIsValid = table.Column<bool>(nullable: false),
                    IsSigned = table.Column<bool>(nullable: false),
                    LicenceUrlIsFWLink = table.Column<bool>(nullable: false),
                    LicenceUrlIsValid = table.Column<bool>(nullable: false),
                    PackageTitle = table.Column<string>(nullable: true),
                    ProjectUrlIsFWLink = table.Column<bool>(nullable: false),
                    ProjectUrlIsValid = table.Column<bool>(nullable: false),
                    TotalDownloads = table.Column<long>(nullable: false),
                    TotalVersions = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.PackageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
