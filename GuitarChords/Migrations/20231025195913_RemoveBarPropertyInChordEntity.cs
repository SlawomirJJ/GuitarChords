using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarChords.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBarPropertyInChordEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bar",
                table: "Chords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bar",
                table: "Chords",
                type: "int",
                nullable: true);
        }
    }
}
