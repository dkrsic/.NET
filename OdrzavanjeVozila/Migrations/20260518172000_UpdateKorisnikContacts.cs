using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdrzavanjeVozila.Migrations
{
    public partial class UpdateKorisnikContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE Korisnici SET Email = 'marko.horvat@autosluzba.hr', Telefon = '+385 91 482 1163' WHERE Id = 1;
UPDATE Korisnici SET Email = 'ana.novak@autosluzba.hr', Telefon = '+385 91 637 2048' WHERE Id = 2;
UPDATE Korisnici SET Email = 'ivan.peric@autosluzba.hr', Telefon = '+385 95 781 9044' WHERE Id = 3;
UPDATE Korisnici SET Email = 'maja.juric@autosluzba.hr', Telefon = '+385 98 520 7712' WHERE Id = 4;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE Korisnici SET Email = 'marko@mail.com', Telefon = '099-1111111' WHERE Id = 1;
UPDATE Korisnici SET Email = 'ana@mail.com', Telefon = '099-2222222' WHERE Id = 2;
UPDATE Korisnici SET Email = 'ivan@mail.com', Telefon = '099-3333333' WHERE Id = 3;
UPDATE Korisnici SET Email = 'maja@mail.com', Telefon = '099-4444444' WHERE Id = 4;
");
        }
    }
}