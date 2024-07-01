using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexCoach.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToPlanEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Plans");
        }
    }
}
