using OdrzavanjeVozila.Tools;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdrzavanjeVozila.Klase
{
    public class Automobil
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Marka { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Godiste { get; set; }
        [Required]
        public string RegistracijskiBroj { get; set; }
        [Required]
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

        // Soft-delete timestamp: null when active
        public DateTime? DeletedAt { get; set; }

        public string Naziv => $"{Marka} {Model} ({Godiste})";
    }
}
