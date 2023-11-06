using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodventureV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UseIndexInsteadAK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SkillTypes_Code",
                table: "SkillTypes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Skills_Code",
                table: "Skills");

            migrationBuilder.CreateIndex(
                name: "IX_SkillTypes_Code",
                table: "SkillTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Code",
                table: "Skills",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SkillTypes_Code",
                table: "SkillTypes");

            migrationBuilder.DropIndex(
                name: "IX_Skills_Code",
                table: "Skills");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SkillTypes_Code",
                table: "SkillTypes",
                column: "Code");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Skills_Code",
                table: "Skills",
                column: "Code");
        }
    }
}
