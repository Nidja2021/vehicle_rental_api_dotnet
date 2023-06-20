using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRental.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Models",
                table: "Vehicles",
                newName: "Model");

            migrationBuilder.AddColumn<bool>(
                name: "Availability",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Vehicles",
                newName: "Models");
        }
    }
}
