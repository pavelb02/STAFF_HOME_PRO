using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personnel.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "work_experiences",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "persons",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "work_experiences",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "persons",
                newName: "Id");
        }
    }
}
