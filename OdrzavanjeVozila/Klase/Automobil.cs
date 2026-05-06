using OdrzavanjeVozila.Tools;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdrzavanjeVozila.Klase
{
    public class Automobil
    {
        [Key]
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Godiste { get; set; }
        public string RegistracijskiBroj { get; set; }
        public string BrojSasije { get; set; }
        public int TrenutnaKilometraza { get; set; }
        public VrstaPogona VrstaPogona { get; set; }
        public DateTime DatumPrvogServisa { get; set; }

        // FK to Korisnik
        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

        // 1-N relationship
        public virtual ICollection<ServisniNalog> ServisniNalozi { get; set; }

        public Automobil()
        {
            ServisniNalozi = new List<ServisniNalog>();
        }

        public string Naziv => $"{Marka} {Model} ({Godiste})";
    }
}
