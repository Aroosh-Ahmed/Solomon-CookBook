using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolomonCookBook.Migrations
{
    public partial class FieldsAddedtoRecepieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Recepies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Recepies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Recepies");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Recepies");
        }
    }
}
