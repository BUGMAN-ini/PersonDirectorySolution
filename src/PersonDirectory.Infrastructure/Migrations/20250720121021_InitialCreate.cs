using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonDirectory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationType = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    RelatedToPersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedPersons_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatedPersons_Persons_RelatedToPersonId",
                        column: x => x.RelatedToPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "Georgia", "Tbilisi" },
                    { 2, "Georgia", "Batumi" },
                    { 3, "Georgia", "Kutaisi" },
                    { 4, "Georgia", "Rustavi" },
                    { 5, "Georgia", "Zugdidi" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "CityId", "DateOfBirth", "FirstName", "Gender", "ImagePath", "LastName", "PersonalNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luka", 1, "C://Users/Documents/Image", "Matiashvili", "01010101010" },
                    { 2, 2, new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nino", 2, "C://Users/Documents/Image", "Beridze", "02020202020" },
                    { 3, 3, new DateTime(1993, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giorgi", 1, "C://Users/Documents/Image", "Kiknavelidze", "03030303030" },
                    { 4, 4, new DateTime(1994, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana", 2, "C://Users/Documents/Image", "Chikvaidze", "04040404040" },
                    { 5, 5, new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dato", 1, "C://Users/Documents/Image", "Gachechiladze", "05050505050" }
                });

            migrationBuilder.InsertData(
                table: "RelatedPersons",
                columns: new[] { "Id", "PersonId", "RelatedToPersonId", "RelationType" },
                values: new object[,]
                {
                    { 1, 1, 2, 5 },
                    { 2, 1, 3, 6 },
                    { 3, 2, 4, 4 },
                    { 4, 3, 5, 7 },
                    { 5, 4, 1, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                table: "Persons",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonalNumber",
                table: "Persons",
                column: "PersonalNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PersonId",
                table: "PhoneNumbers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersons_PersonId",
                table: "RelatedPersons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersons_RelatedToPersonId",
                table: "RelatedPersons",
                column: "RelatedToPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "RelatedPersons");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
