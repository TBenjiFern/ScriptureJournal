using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScriptureJournal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scripture",
                columns: table => new
                {
                    ScriptureId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Book = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Chapter = table.Column<string>(type: "TEXT", nullable: false),
                    Verse = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripture", x => x.ScriptureId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scripture");
        }
    }
}
