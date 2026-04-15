using OdrzavanjeVozila.Tools;

namespace OdrzavanjeVozila.Klase
{
    public class Automobil
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Godiste { get; set; }
        public string RegistracijskiBroj { get; set; }
        public string BrojSasije { get; set; }
        public int TrenutnaKilometraza { get; set; }
        public VrstaPogona VrstaPogona { get; set; }
        public DateTime DatumPrvogServisa { get; set; }

     
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public List<ServisniNalog> ServisniNalozi { get; set; }

        public Automobil()
        {
            ServisniNalozi = new List<ServisniNalog>();
        }

        public string Naziv => $"{Marka} {Model} ({Godiste})";
    }
}
