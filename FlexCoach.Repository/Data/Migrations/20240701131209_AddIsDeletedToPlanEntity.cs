using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexCoach.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToPlanEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Plans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Plans");
        }
    }
}
