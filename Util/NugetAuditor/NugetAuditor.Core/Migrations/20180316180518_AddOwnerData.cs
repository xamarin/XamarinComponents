using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NugetAuditor.Core.Migrations
{
    public partial class AddOwnerData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasMicrosoftOwner",
                table: "Results",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Owners",
                table: "Results",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMicrosoftOwner",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Owners",
                table: "Results");
        }
    }
}
