using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodventureV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProficiency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Proficiency",
                table: "PlayerSkill",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Proficiency",
                table: "PlayerSkill");
        }
    }
}
