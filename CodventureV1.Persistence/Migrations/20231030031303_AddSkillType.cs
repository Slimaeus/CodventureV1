using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodventureV1.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Skills",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SkillTypeId",
                table: "Skills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SkillTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillTypeId",
                table: "Skills",
                column: "SkillTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_SkillTypes_SkillTypeId",
                table: "Skills",
                column: "SkillTypeId",
                principalTable: "SkillTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_SkillTypes_SkillTypeId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "SkillTypes");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SkillTypeId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SkillTypeId",
                table: "Skills");
        }
    }
}
