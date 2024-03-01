using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSkill",
                table: "Skills",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdPerson",
                table: "Persons",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Skills",
                newName: "IdSkill");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Persons",
                newName: "IdPerson");
        }
    }
}
