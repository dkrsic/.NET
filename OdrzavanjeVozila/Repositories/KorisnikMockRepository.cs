using OdrzavanjeVozila.Tools;
using OdrzavanjeVozila.Klase;

namespace OdrzavanjeVozila.Repositories
{
    public class KorisnikMockRepository
    {
        private readonly List<Korisnik> _korisnici;

        public KorisnikMockRepository()
        {
            _korisnici = new List<Korisnik>
            {
                new Korisnik { Id = 1, Ime = "Ivan", Prezime = "Horvat", Email = "ivan.horvat@email.com", Telefon = "091-123-4567", Adresa = "Ilica 10, Zagreb", DatumRegistracije = new DateTime(2022, 3, 15) },
                new Korisnik { Id = 2, Ime = "Marija", Prezime = "Kovač", Email = "marija.kovac@email.com", Telefon = "092-234-5678", Adresa = "Vukovarska 5, Split", DatumRegistracije = new DateTime(2021, 7, 20) },
                new Korisnik { Id = 3, Ime = "Tomislav", Prezime = "Babić", Email = "tomislav.babic@email.com", Telefon = "095-345-6789", Adresa = "Kapucinska 3, Osijek", DatumRegistracije = new DateTime(2023, 1, 10) },
                new Korisnik { Id = 4, Ime = "Ana", Prezime = "Novak", Email = "ana.novak@email.com", Telefon = "098-456-7890", Adresa = "Korzo 8, Rijeka", DatumRegistracije = new DateTime(2020, 11, 5) },
            };
        }

        public List<Korisnik> GetAll() => _korisnici;

        public Korisnik? GetById(int id) => _korisnici.FirstOrDefault(k => k.Id == id);
    }
}