using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObourLand.Migrations
{
    /// <inheritdoc />
    public partial class addsupervisorIdcoluser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Users");
        }
    }
}
