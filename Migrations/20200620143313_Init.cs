using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cw13.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    IdKlient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.IdKlient);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    IdPacownika = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.IdPacownika);
                });

            migrationBuilder.CreateTable(
                name: "WyrobyCukiernicze",
                columns: table => new
                {
                    IdWyrobuCukierniczego = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: true),
                    CenaZaSzt = table.Column<float>(nullable: false),
                    Typ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WyrobyCukiernicze", x => x.IdWyrobuCukierniczego);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienia",
                columns: table => new
                {
                    IdZamowienia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPrzyjecia = table.Column<DateTime>(nullable: false),
                    DataRealizacji = table.Column<DateTime>(nullable: false),
                    Uwagi = table.Column<string>(nullable: true),
                    KlientIdKlient = table.Column<int>(nullable: true),
                    PracownikIdPacownika = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienia", x => x.IdZamowienia);
                    table.ForeignKey(
                        name: "FK_Zamowienia_Klienci_KlientIdKlient",
                        column: x => x.KlientIdKlient,
                        principalTable: "Klienci",
                        principalColumn: "IdKlient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zamowienia_Pracownicy_PracownikIdPacownika",
                        column: x => x.PracownikIdPacownika,
                        principalTable: "Pracownicy",
                        principalColumn: "IdPacownika",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZamowieniaWyrobyCukiernicze",
                columns: table => new
                {
                    IdWyrobCukierniczy = table.Column<int>(nullable: false),
                    IdZamowienie = table.Column<int>(nullable: false),
                    Ilosc = table.Column<int>(nullable: false),
                    Uwagi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZamowieniaWyrobyCukiernicze", x => new { x.IdZamowienie, x.IdWyrobCukierniczy });
                    table.ForeignKey(
                        name: "FK_ZamowieniaWyrobyCukiernicze_WyrobyCukiernicze_IdWyrobCukierniczy",
                        column: x => x.IdWyrobCukierniczy,
                        principalTable: "WyrobyCukiernicze",
                        principalColumn: "IdWyrobuCukierniczego",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZamowieniaWyrobyCukiernicze_Zamowienia_IdZamowienie",
                        column: x => x.IdZamowienie,
                        principalTable: "Zamowienia",
                        principalColumn: "IdZamowienia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_KlientIdKlient",
                table: "Zamowienia",
                column: "KlientIdKlient");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_PracownikIdPacownika",
                table: "Zamowienia",
                column: "PracownikIdPacownika");

            migrationBuilder.CreateIndex(
                name: "IX_ZamowieniaWyrobyCukiernicze_IdWyrobCukierniczy",
                table: "ZamowieniaWyrobyCukiernicze",
                column: "IdWyrobCukierniczy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZamowieniaWyrobyCukiernicze");

            migrationBuilder.DropTable(
                name: "WyrobyCukiernicze");

            migrationBuilder.DropTable(
                name: "Zamowienia");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Pracownicy");
        }
    }
}
