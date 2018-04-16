using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NugetAuditor.Core.Migrations
{
    public partial class ComponentStoreCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LicenceUrlPointsToComponentsStore",
                table: "Results",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProjectUrlPointsToComponentsStore",
                table: "Results",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenceUrlPointsToComponentsStore",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "ProjectUrlPointsToComponentsStore",
                table: "Results");
        }
    }
}
