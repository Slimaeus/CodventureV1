using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodventureV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintForSkillCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_SkillTypes_Code",
                table: "SkillTypes",
                column: "Code");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Skills_Code",
                table: "Skills",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SkillTypes_Code",
                table: "SkillTypes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Skills_Code",
                table: "Skills");
        }
    }
}
