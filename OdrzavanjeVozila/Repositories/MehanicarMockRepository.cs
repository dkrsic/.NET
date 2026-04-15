using OdrzavanjeVozila.Tools;
using OdrzavanjeVozila.Klase;

namespace OdrzavanjeVozila.Repositories
{
    public class MehanicarMockRepository
    {
        private readonly List<Mehanicar> _mehanicari;

        public MehanicarMockRepository()
        {
            _mehanicari = new List<Mehanicar>
            {
                new Mehanicar { Id = 1, Ime = "Krešimir", Prezime = "Jurić", Specijalizacija = "Motor i mjenjač", DatumZaposlenja = new DateTime(2015, 4, 1), SatnicaEUR = 18.5m, RadionicaId = 1 },
                new Mehanicar { Id = 2, Ime = "Darko", Prezime = "Blažević", Specijalizacija = "Elektrika i elektronika", DatumZaposlenja = new DateTime(2018, 9, 15), SatnicaEUR = 20.0m, RadionicaId = 1 },
                new Mehanicar { Id = 3, Ime = "Stjepan", Prezime = "Matić", Specijalizacija = "Karoserija i lakiranje", DatumZaposlenja = new DateTime(2020, 2, 1), SatnicaEUR = 16.0m, RadionicaId = 2 },
                new Mehanicar { Id = 4, Ime = "Luka", Prezime = "Perić", Specijalizacija = "Kočioni sustav i ovjes", DatumZaposlenja = new DateTime(2017, 6, 10), SatnicaEUR = 17.5m, RadionicaId = 2 },
            };
        }

        public List<Mehanicar> GetAll() => _mehanicari;
        public Mehanicar? GetById(int id) => _mehanicari.FirstOrDefault(m => m.Id == id);
        public List<Mehanicar> GetByRadionicaId(int radionicaId) => _mehanicari.Where(m => m.RadionicaId == radionicaId).ToList();
    }
}