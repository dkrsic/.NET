using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdrzavanjeVozila.Klase
{
    public class Mehanicar
    {
        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Specijalizacija { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public decimal SatnicaEUR { get; set; }

        // FK to Radionica
        public int RadionicaId { get; set; }
        public virtual Radionica Radionica { get; set; }

        // 1-N relationship
        public virtual ICollection<ServisniNalog> Nalozi { get; set; }

        public Mehanicar()
        {
            Nalozi = new List<ServisniNalog>();
        }

        public string PunoIme => $"{Ime} {Prezime}";
    }
}
