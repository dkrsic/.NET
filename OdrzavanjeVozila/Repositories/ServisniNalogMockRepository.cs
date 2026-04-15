using OdrzavanjeVozila.Tools;
using OdrzavanjeVozila.Klase;

namespace OdrzavanjeVozila.Repositories
{
    public class ServisniNalogMockRepository
    {
        private readonly List<ServisniNalog> _nalozi;

        public ServisniNalogMockRepository()
        {
            _nalozi = new List<ServisniNalog>
            {
                new ServisniNalog
                {
                    Id = 1, DatumOtvaranja = new DateTime(2024, 1, 10), DatumZatvaranja = new DateTime(2024, 1, 11),
                    OpisRadova = "Redovni servis - zamjena ulja i filtera", UkupnaCijena = 85.00m,
                    Status = StatusNaloga.Zavrsen, KilometrazaPrilikomServisa = 80000,
                    Napomena = "Preporučena zamjena guma na sljedećem servisu",
                    SljedecaPreporucenaPregleda = new DateTime(2024, 7, 10),
                    AutomobilId = 1, MehanicarId = 1,
                    Stavke = new List<NalogStavka>
                    {
                        new NalogStavka { Id = 1, NalogId = 1, DioId = 1, Kolicina = 1, CijenaKomad = 12.50m, Napomena = "Standardni filter" }
                    }
                },
                new ServisniNalog
                {
                    Id = 2, DatumOtvaranja = new DateTime(2024, 3, 5),
                    OpisRadova = "Zamjena kočionih pločica i provjera kočionog sustava", UkupnaCijena = 165.00m,
                    Status = StatusNaloga.UObradi, KilometrazaPrilikomServisa = 82500,
                    AutomobilId = 1, MehanicarId = 4,
                    Stavke = new List<NalogStavka>
                    {
                        new NalogStavka { Id = 2, NalogId = 2, DioId = 2, Kolicina = 2, CijenaKomad = 45.00m, Napomena = "Prednje i stražnje" }
                    }
                },
                new ServisniNalog
                {
                    Id = 3, DatumOtvaranja = new DateTime(2024, 2, 20), DatumZatvaranja = new DateTime(2024, 2, 21),
                    OpisRadova = "Dijagnostika i zamjena alternatora", UkupnaCijena = 280.00m,
                    Status = StatusNaloga.Zavrsen, KilometrazaPrilikomServisa = 105000,
                    AutomobilId = 3, MehanicarId = 2,
                    Stavke = new List<NalogStavka>
                    {
                        new NalogStavka { Id = 3, NalogId = 3, DioId = 4, Kolicina = 1, CijenaKomad = 210.00m }
                    }
                },
                new ServisniNalog
                {
                    Id = 4, DatumOtvaranja = new DateTime(2024, 4, 1),
                    OpisRadova = "Pregled i zamjena amortizera", UkupnaCijena = 390.00m,
                    Status = StatusNaloga.Otvoren, KilometrazaPrilikomServisa = 65000,
                    AutomobilId = 4, MehanicarId = 4,
                    Stavke = new List<NalogStavka>
                    {
                        new NalogStavka { Id = 4, NalogId = 4, DioId = 3, Kolicina = 2, CijenaKomad = 120.00m }
                    }
                },
                new ServisniNalog
                {
                    Id = 5, DatumOtvaranja = new DateTime(2024, 3, 18), DatumZatvaranja = new DateTime(2024, 3, 18),
                    OpisRadova = "Godišnji tehnički pregled - priprema", UkupnaCijena = 55.00m,
                    Status = StatusNaloga.Zavrsen, KilometrazaPrilikomServisa = 27000,
                    AutomobilId = 5, MehanicarId = 2,
                    Stavke = new List<NalogStavka>()
                },
            };
        }

        public List<ServisniNalog> GetAll() => _nalozi;
        public ServisniNalog? GetById(int id) => _nalozi.FirstOrDefault(n => n.Id == id);
        public List<ServisniNalog> GetByAutomobilId(int automobilId) => _nalozi.Where(n => n.AutomobilId == automobilId).ToList();
        public List<ServisniNalog> GetByMehanicarId(int mehanicarId) => _nalozi.Where(n => n.MehanicarId == mehanicarId).ToList();
    }
}