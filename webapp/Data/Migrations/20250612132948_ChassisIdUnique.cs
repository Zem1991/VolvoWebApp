using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolvoWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChassisIdUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChassisSeries",
                table: "Vehicle",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Vehicle_ChassisSeries_ChassisNumber",
                table: "Vehicle",
                columns: new[] { "ChassisSeries", "ChassisNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Vehicle_ChassisSeries_ChassisNumber",
                table: "Vehicle");

            migrationBuilder.AlterColumn<string>(
                name: "ChassisSeries",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
