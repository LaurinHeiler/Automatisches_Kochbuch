using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Automatisches_Kochbuch.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tab_rezepte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Rezeptname = table.Column<string>(type: "varchar(100)", nullable: false),
                    vegetarisch = table.Column<bool>(type: "boolean", nullable: false),
                    vegan = table.Column<bool>(type: "boolean", nullable: false),
                    glutenfrei = table.Column<bool>(type: "boolean", nullable: false),
                    Zubereitung = table.Column<string>(type: "text", nullable: false),
                    Bild = table.Column<byte[]>(type: "mediumblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_rezepte", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tab_user",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Vorname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nachname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Passwort = table.Column<string>(type: "varchar(50)", nullable: false),
                    Role = table.Column<string>(nullable: false),
                    AnzahlPortionen = table.Column<int>(name: "Anzahl Portionen", type: "int(11)", nullable: false, defaultValueSql: "'1'"),
                    Vegetarier = table.Column<bool>(type: "boolean", nullable: false),
                    Veganer = table.Column<bool>(type: "boolean", nullable: false),
                    glutenfrei = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_user", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tab_zutaten",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Zutat = table.Column<string>(type: "varchar(50)", nullable: false),
                    ID_Zutat_Einheit = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Zutat_Kategorie = table.Column<int>(type: "int(11)", nullable: false),
                    vegetarisch = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "'1'"),
                    vegan = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "'1'"),
                    glutenfrei = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_zutaten", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tab_zutaten_einheit",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Einheit_Kuerzel = table.Column<string>(type: "varchar(15)", nullable: false),
                    Einheit = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_zutaten_einheit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tab_zutaten_kategorien",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Kategorie = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_zutaten_kategorien", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "lnk_tab_user_rezepte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_user = table.Column<int>(type: "int(11)", nullable: false),
                    ID_rezepte = table.Column<int>(type: "int(11)", nullable: false),
                    Anzahl_Portionen = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lnk_tab_user_rezepte", x => x.ID);
                    table.ForeignKey(
                        name: "lnk_tab_user_rezepte_ibfk_1",
                        column: x => x.ID_rezepte,
                        principalTable: "tab_rezepte",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "lnk_tab_user_rezepte_ibfk_2",
                        column: x => x.ID_user,
                        principalTable: "tab_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lnk_tab_rezept_zutaten",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_rezept = table.Column<int>(type: "int(11)", nullable: false),
                    ID_zutaten = table.Column<int>(type: "int(11)", nullable: false),
                    Menge = table.Column<decimal>(type: "decimal(11,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lnk_tab_rezept_zutaten", x => x.ID);
                    table.ForeignKey(
                        name: "lnk_tab_rezept_zutaten_ibfk_1",
                        column: x => x.ID_rezept,
                        principalTable: "tab_rezepte",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "lnk_tab_rezept_zutaten_ibfk_2",
                        column: x => x.ID_zutaten,
                        principalTable: "tab_zutaten",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lnk_tab_user_zutaten",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_user = table.Column<int>(type: "int(11)", nullable: false),
                    ID_zutaten = table.Column<int>(type: "int(11)", nullable: false),
                    Menge = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lnk_tab_user_zutaten", x => x.ID);
                    table.ForeignKey(
                        name: "lnk_tab_user_zutaten_ibfk_1",
                        column: x => x.ID_user,
                        principalTable: "tab_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "lnk_tab_user_zutaten_ibfk_2",
                        column: x => x.ID_zutaten,
                        principalTable: "tab_zutaten",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_rezept",
                table: "lnk_tab_rezept_zutaten",
                column: "ID_rezept");

            migrationBuilder.CreateIndex(
                name: "fk_zutaten",
                table: "lnk_tab_rezept_zutaten",
                column: "ID_zutaten");

            migrationBuilder.CreateIndex(
                name: "fk_rezepte",
                table: "lnk_tab_user_rezepte",
                column: "ID_rezepte");

            migrationBuilder.CreateIndex(
                name: "fk_user",
                table: "lnk_tab_user_rezepte",
                column: "ID_user");

            migrationBuilder.CreateIndex(
                name: "fk_user",
                table: "lnk_tab_user_zutaten",
                column: "ID_user");

            migrationBuilder.CreateIndex(
                name: "fk_zutaten",
                table: "lnk_tab_user_zutaten",
                column: "ID_zutaten");

            migrationBuilder.CreateIndex(
                name: "Rezeptname",
                table: "tab_rezepte",
                column: "Rezeptname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Zutat",
                table: "tab_zutaten",
                column: "Zutat",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lnk_tab_rezept_zutaten");

            migrationBuilder.DropTable(
                name: "lnk_tab_user_rezepte");

            migrationBuilder.DropTable(
                name: "lnk_tab_user_zutaten");

            migrationBuilder.DropTable(
                name: "tab_zutaten_einheit");

            migrationBuilder.DropTable(
                name: "tab_zutaten_kategorien");

            migrationBuilder.DropTable(
                name: "tab_rezepte");

            migrationBuilder.DropTable(
                name: "tab_user");

            migrationBuilder.DropTable(
                name: "tab_zutaten");
        }
    }
}
