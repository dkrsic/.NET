using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OdrzavanjeVozila.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [Radionice] ON;
INSERT INTO [Radionice] ([Id], [Adresa], [Email], [Naziv], [Telefon])
VALUES (3, 'Riva 12, Rijeka', 'kontakt.rijeka@radionica-mreza.hr', 'Radionica Rijeka', '+385 51 642 880');
SET IDENTITY_INSERT [Radionice] OFF;

SET IDENTITY_INSERT [Korisnici] ON;
INSERT INTO [Korisnici] ([Id], [Adresa], [DatumRegistracije], [Email], [Ime], [Prezime], [Telefon])
VALUES
    (3, 'Korzo 15, Rijeka', '2025-03-08T00:00:00', 'ivan.peric@autosluzba.hr', 'Ivan', 'Peric', '+385 95 781 9044'),
    (4, 'Dubrovacka 21, Zagreb', '2025-04-02T00:00:00', 'maja.juric@autosluzba.hr', 'Maja', 'Juric', '+385 98 520 7712');
SET IDENTITY_INSERT [Korisnici] OFF;

SET IDENTITY_INSERT [Dijelovi] ON;
INSERT INTO [Dijelovi] ([Id], [Cijena], [KatalogBroj], [Kategorija], [KolicinaNaSkladistu], [Naziv], [Opis], [Proizvodac])
VALUES
    (3, 89.90, 'BAT-014', 3, 12, 'Akumulator', 'Start-stop akumulator', 'Varta'),
    (4, 12.75, 'FLT-022', 0, 18, 'Filter zraka', 'Motorni filter zraka', 'Mann'),
    (5, 79.99, 'TIR-20555', 5, 8, 'Gume 205/55 R16', 'Ljetni komplet guma', 'Michelin');
SET IDENTITY_INSERT [Dijelovi] OFF;

SET IDENTITY_INSERT [Mehanicari] ON;
INSERT INTO [Mehanicari] ([Id], [DatumZaposlenja], [Ime], [Prezime], [RadionicaId], [SatnicaEUR], [Specijalizacija])
VALUES
    (4, '2021-11-05T00:00:00', 'Stjepan', 'Boric', 3, 27.50, 'Klimatizacija'),
    (5, '2024-02-20T00:00:00', 'Filip', 'Maric', 3, 32.00, 'Dijagnostika');
SET IDENTITY_INSERT [Mehanicari] OFF;

SET IDENTITY_INSERT [Automobili] ON;
INSERT INTO [Automobili] ([Id], [BrojSasije], [DatumPrvogServisa], [Godiste], [KorisnikId], [Marka], [Model], [RegistracijskiBroj], [TrenutnaKilometraza], [VrstaPogona])
VALUES
    (3, 'W0L0AHL48J1234567', '2024-08-12T00:00:00', 2018, 3, 'Opel', 'Astra', 'RI-345-EF', 78000, 4),
    (4, 'JTNB43BE003123456', '2025-01-15T00:00:00', 2021, 4, 'Toyota', 'Corolla', 'ZG-890-GH', 21000, 3);
SET IDENTITY_INSERT [Automobili] OFF;

SET IDENTITY_INSERT [ServisniNalozi] ON;
INSERT INTO [ServisniNalozi] ([Id], [AutomobilId], [DatumOtvaranja], [DatumZatvaranja], [KilometrazaPrilikomServisa], [MehanicarId], [Napomena], [OpisRadova], [SljedecaPreporucenaPregleda], [Status], [UkupnaCijena])
VALUES
    (1, 1, '2026-01-10T00:00:00', '2026-01-12T00:00:00', 45250, 1, 'Vozilo uredno, preporucena kontrola za 6 mjeseci', 'Redovni servis i zamjena ulja', '2026-07-10T00:00:00', 2, 96.97),
    (2, 2, '2026-02-03T00:00:00', NULL, 32220, 2, 'Ceka potvrdu narudzbe dodatnog dijela', 'Dijagnostika elektrike i zamjena akumulatora', '2026-08-03T00:00:00', 1, 89.90),
    (3, 3, '2026-03-18T00:00:00', '2026-03-19T00:00:00', 78120, 3, 'Kocnice testirane i ispravne', 'Servis kocnica i zamjena plocica', '2026-09-18T00:00:00', 2, 112.45),
    (4, 4, '2026-04-07T00:00:00', NULL, 21400, 4, 'Ceka dolazak vozila u radionicu', 'Zamjena guma i sezonski pregled', NULL, 0, 79.99);
SET IDENTITY_INSERT [ServisniNalozi] OFF;

SET IDENTITY_INSERT [NalogStavke] ON;
INSERT INTO [NalogStavke] ([Id], [CijenaKomad], [DioId], [Kolicina], [NalogId], [Napomena])
VALUES
    (1, 15.99, 1, 1, 1, 'Motorno ulje 5W-30'),
    (2, 25.50, 2, 4, 1, 'Prednji set kocionih plocica'),
    (3, 89.90, 3, 1, 2, 'Start-stop akumulator'),
    (4, 12.75, 4, 2, 3, 'Filteri za zrak'),
    (5, 79.99, 5, 4, 4, 'Set ljetnih guma');
SET IDENTITY_INSERT [NalogStavke] OFF;
" );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [NalogStavke] WHERE [Id] IN (1, 2, 3, 4, 5);
DELETE FROM [ServisniNalozi] WHERE [Id] IN (1, 2, 3, 4);
DELETE FROM [Automobili] WHERE [Id] IN (3, 4);
DELETE FROM [Mehanicari] WHERE [Id] IN (4, 5);
DELETE FROM [Dijelovi] WHERE [Id] IN (3, 4, 5);
DELETE FROM [Korisnici] WHERE [Id] IN (3, 4);
DELETE FROM [Radionice] WHERE [Id] = 3;
");
        }
    }
}
