using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personnel.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    last_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    avatar_url = table.Column<string>(type: "text", nullable: true, defaultValue: "https://i.ibb.co/000000/avatar.png"),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "work_experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    position = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    organization = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    city = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    country = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: true),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_work_experiences_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_work_experiences_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_persons_email",
                table: "persons",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_persons_phone",
                table: "persons",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_work_experiences_person_id",
                table: "work_experiences",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_experiences_PersonId",
                table: "work_experiences",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "work_experiences");

            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}
