using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObourLand.Migrations
{
    /// <inheritdoc />
    public partial class addIsActiveGroupTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Groups",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Groups");
        }
    }
}
