using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personnel.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "avatar_url",
                table: "persons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "https://i.ibb.co/000000/avatar.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "avatar_url",
                table: "persons",
                type: "text",
                nullable: true,
                defaultValue: "https://i.ibb.co/000000/avatar.png",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
