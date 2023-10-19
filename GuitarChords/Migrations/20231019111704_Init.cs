using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarChords.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChordName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Bar = table.Column<int>(type: "int", nullable: true),
                    FirstString = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    SecondString = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ThirdString = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    FourthString = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    FifthString = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    SixthString = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chords");
        }
    }
}
