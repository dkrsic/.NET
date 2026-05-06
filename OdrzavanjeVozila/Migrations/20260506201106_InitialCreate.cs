using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OdrzavanjeVozila.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dijelovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KatalogBroj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proizvodac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Kategorija = table.Column<int>(type: "int", nullable: false),
                    KolicinaNaSkladistu = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dijelovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRegistracije = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Radionice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radionice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Automobili",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    RegistracijskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojSasije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrenutnaKilometraza = table.Column<int>(type: "int", nullable: false),
                    VrstaPogona = table.Column<int>(type: "int", nullable: false),
                    DatumPrvogServisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobili", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Automobili_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mehanicari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specijalizacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SatnicaEUR = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RadionicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mehanicari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mehanicari_Radionice_RadionicaId",
                        column: x => x.RadionicaId,
                        principalTable: "Radionice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServisniNalozi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumOtvaranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZatvaranja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SljedecaPreporucenaPregleda = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpisRadova = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UkupnaCijena = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    KilometrazaPrilikomServisa = table.Column<int>(type: "int", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutomobilId = table.Column<int>(type: "int", nullable: false),
                    MehanicarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServisniNalozi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServisniNalozi_Automobili_AutomobilId",
                        column: x => x.AutomobilId,
                        principalTable: "Automobili",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServisniNalozi_Mehanicari_MehanicarId",
                        column: x => x.MehanicarId,
                        principalTable: "Mehanicari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NalogStavke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    CijenaKomad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NalogId = table.Column<int>(type: "int", nullable: false),
                    DioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NalogStavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NalogStavke_Dijelovi_DioId",
                        column: x => x.DioId,
                        principalTable: "Dijelovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NalogStavke_ServisniNalozi_NalogId",
                        column: x => x.NalogId,
                        principalTable: "ServisniNalozi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dijelovi",
                columns: new[] { "Id", "Cijena", "KatalogBroj", "Kategorija", "KolicinaNaSkladistu", "Naziv", "Opis", "Proizvodac" },
                values: new object[,]
                {
                    { 1, 15.99m, "OIL-001", 0, 50, "Ulje motora", "Sintetsko motorno ulje", "Castrol" },
                    { 2, 25.50m, "BRK-001", 1, 30, "Kocione plocice", "Set prednjih kocionih plocica", "Bosch" }
                });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "Id", "Adresa", "DatumRegistracije", "Email", "Ime", "Prezime", "Telefon" },
                values: new object[,]
                {
                    { 1, "Ilica 200, Zagreb", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "marko@mail.com", "Marko", "Horvat", "099-1111111" },
                    { 2, "Gunduliceva 50, Split", new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "ana@mail.com", "Ana", "Novak", "099-2222222" }
                });

            migrationBuilder.InsertData(
                table: "Radionice",
                columns: new[] { "Id", "Adresa", "Email", "Naziv", "Telefon" },
                values: new object[,]
                {
                    { 1, "Ilica 100, Zagreb", "info@radionica-zg.hr", "Radionica Zagreb", "01-1234567" },
                    { 2, "Marmontova 50, Split", "info@radionica-split.hr", "Radionica Split", "021-1234567" }
                });

            migrationBuilder.InsertData(
                table: "Automobili",
                columns: new[] { "Id", "BrojSasije", "DatumPrvogServisa", "Godiste", "KorisnikId", "Marka", "Model", "RegistracijskiBroj", "TrenutnaKilometraza", "VrstaPogona" },
                values: new object[,]
                {
                    { 1, "WVWZZZ3CZ9E123456", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2019, 1, "Volkswagen", "Golf", "ZG-234-AB", 45000, 0 },
                    { 2, "WBXYZ0000000000001", new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2020, 2, "BMW", "320", "ZG-567-CD", 32000, 1 }
                });

            migrationBuilder.InsertData(
                table: "Mehanicari",
                columns: new[] { "Id", "DatumZaposlenja", "Ime", "Prezime", "RadionicaId", "SatnicaEUR", "Specijalizacija" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Petar", "Kovac", 1, 25.00m, "Mehanika" },
                    { 2, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jovan", "Jovanovic", 1, 30.00m, "Elektrika" },
                    { 3, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Milan", "Milanovic", 2, 28.00m, "Mehanika" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_KorisnikId",
                table: "Automobili",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Mehanicari_RadionicaId",
                table: "Mehanicari",
                column: "RadionicaId");

            migrationBuilder.CreateIndex(
                name: "IX_NalogStavke_DioId",
                table: "NalogStavke",
                column: "DioId");

            migrationBuilder.CreateIndex(
                name: "IX_NalogStavke_NalogId",
                table: "NalogStavke",
                column: "NalogId");

            migrationBuilder.CreateIndex(
                name: "IX_ServisniNalozi_AutomobilId",
                table: "ServisniNalozi",
                column: "AutomobilId");

            migrationBuilder.CreateIndex(
                name: "IX_ServisniNalozi_MehanicarId",
                table: "ServisniNalozi",
                column: "MehanicarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NalogStavke");

            migrationBuilder.DropTable(
                name: "Dijelovi");

            migrationBuilder.DropTable(
                name: "ServisniNalozi");

            migrationBuilder.DropTable(
                name: "Automobili");

            migrationBuilder.DropTable(
                name: "Mehanicari");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Radionice");
        }
    }
}
