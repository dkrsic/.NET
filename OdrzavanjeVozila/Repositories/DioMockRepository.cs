using OdrzavanjeVozila.Tools;
using OdrzavanjeVozila.Klase;

namespace OdrzavanjeVozila.Repositories
{
    public class DioMockRepository
    {
        private readonly List<Dio> _dijelovi;

        public DioMockRepository()
        {
            _dijelovi = new List<Dio>
            {
                new Dio { Id = 1, Naziv = "Filter ulja", KatalogBroj = "OIL-001", Proizvodac = "Mann", Cijena = 12.50m, Kategorija = KategorijaDijela.Motor, KolicinaNaSkladistu = 25, Opis = "Filter ulja za benzinske i diesel motore" },
                new Dio { Id = 2, Naziv = "Prednje kočione pločice", KatalogBroj = "BRK-101", Proizvodac = "Brembo", Cijena = 45.00m, Kategorija = KategorijaDijela.Kocnice, KolicinaNaSkladistu = 12, Opis = "Sportske kočione pločice za prednju osovinu" },
                new Dio { Id = 3, Naziv = "Amortizer prednji lijevi", KatalogBroj = "SUS-201", Proizvodac = "Bilstein", Cijena = 120.00m, Kategorija = KategorijaDijela.Ovjes, KolicinaNaSkladistu = 6, Opis = "Plinski amortizer za prednju lijevu stranu" },
                new Dio { Id = 4, Naziv = "Alternator", KatalogBroj = "ELC-301", Proizvodac = "Bosch", Cijena = 210.00m, Kategorija = KategorijaDijela.Elektrika, KolicinaNaSkladistu = 4, Opis = "Alternator 14V 120A" },
                new Dio { Id = 5, Naziv = "Ljetne gume 205/55 R16", KatalogBroj = "TYR-401", Proizvodac = "Michelin", Cijena = 89.00m, Kategorija = KategorijaDijela.Gume, KolicinaNaSkladistu = 16, Opis = "Ljetne gume klase A za mokru cestu" },
                new Dio { Id = 6, Naziv = "Svjećice komplet", KatalogBroj = "MOT-002", Proizvodac = "NGK", Cijena = 32.00m, Kategorija = KategorijaDijela.Motor, KolicinaNaSkladistu = 20, Opis = "Iridium svjećice, komplet 4 kom" },
            };
        }

        public List<Dio> GetAll() => _dijelovi;
        public Dio? GetById(int id) => _dijelovi.FirstOrDefault(d => d.Id == id);
        public List<Dio> GetByKategorija(KategorijaDijela kategorija) => _dijelovi.Where(d => d.Kategorija == kategorija).ToList();
    }
}