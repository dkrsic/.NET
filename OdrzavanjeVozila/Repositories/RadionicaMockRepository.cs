using OdrzavanjeVozila.Tools;
using OdrzavanjeVozila.Klase;

namespace OdrzavanjeVozila.Repositories
{
    public class RadionicaMockRepository
    {
        private readonly List<Radionica> _radionice;

        public RadionicaMockRepository()
        {
            _radionice = new List<Radionica>
            {
                new Radionica { Id = 1, Naziv = "AutoServis Zagreb", Adresa = "Slavonska avenija 22, Zagreb", Telefon = "01-234-5678", Email = "info@autoservis-zg.hr" },
                new Radionica { Id = 2, Naziv = "Moto Centar Split", Adresa = "Domovinskog rata 45, Split", Telefon = "021-345-6789", Email = "kontakt@motocentar-st.hr" },
            };
        }

        public List<Radionica> GetAll() => _radionice;
        public Radionica? GetById(int id) => _radionice.FirstOrDefault(r => r.Id == id);
    }
}