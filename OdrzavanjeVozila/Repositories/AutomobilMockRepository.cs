using OdrzavanjeVozila.Tools;
using OdrzavanjeVozila.Klase;

namespace OdrzavanjeVozila.Repositories
{
    public class AutomobilMockRepository
    {
        private readonly List<Automobil> _automobili;

        public AutomobilMockRepository()
        {
            _automobili = new List<Automobil>
            {
                new Automobil { Id = 1, Marka = "Volkswagen", Model = "Golf", Godiste = 2019, RegistracijskiBroj = "ZG-123-AB", BrojSasije = "WVW12345678901234", TrenutnaKilometraza = 85000, VrstaPogona = VrstaPogona.Dizel, DatumPrvogServisa = new DateTime(2019, 6, 1), KorisnikId = 1 },
                new Automobil { Id = 2, Marka = "Toyota", Model = "Corolla", Godiste = 2021, RegistracijskiBroj = "ZG-456-CD", BrojSasije = "JTDBR32E123456789", TrenutnaKilometraza = 42000, VrstaPogona = VrstaPogona.Hibridni, DatumPrvogServisa = new DateTime(2021, 3, 15), KorisnikId = 1 },
                new Automobil { Id = 3, Marka = "Ford", Model = "Focus", Godiste = 2018, RegistracijskiBroj = "ST-789-EF", BrojSasije = "WF0FXXGBBFJA12345", TrenutnaKilometraza = 110000, VrstaPogona = VrstaPogona.Benzin, DatumPrvogServisa = new DateTime(2018, 9, 20), KorisnikId = 2 },
                new Automobil { Id = 4, Marka = "BMW", Model = "320d", Godiste = 2020, RegistracijskiBroj = "OS-321-GH", BrojSasije = "WBA8D9C50JA123456", TrenutnaKilometraza = 67000, VrstaPogona = VrstaPogona.Dizel, DatumPrvogServisa = new DateTime(2020, 1, 10), KorisnikId = 3 },
                new Automobil { Id = 5, Marka = "Tesla", Model = "Model 3", Godiste = 2022, RegistracijskiBroj = "RI-654-IJ", BrojSasije = "5YJ3E1EA1NF123456", TrenutnaKilometraza = 28000, VrstaPogona = VrstaPogona.Elektricni, DatumPrvogServisa = new DateTime(2022, 5, 5), KorisnikId = 4 },
            };
        }

        public List<Automobil> GetAll() => _automobili;
        public Automobil? GetById(int id) => _automobili.FirstOrDefault(a => a.Id == id);
        public List<Automobil> GetByKorisnikId(int korisnikId) => _automobili.Where(a => a.KorisnikId == korisnikId).ToList();
    }
}