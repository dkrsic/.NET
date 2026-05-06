using OdrzavanjeVozila.Tools;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdrzavanjeVozila.Klase
{
    public class ServisniNalog
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumOtvaranja { get; set; }
        public DateTime? DatumZatvaranja { get; set; }
        public DateTime? SljedecaPreporucenaPregleda { get; set; }
        public string OpisRadova { get; set; }
        public decimal UkupnaCijena { get; set; }
        public StatusNaloga Status { get; set; }
        public int KilometrazaPrilikomServisa { get; set; }
        public string Napomena { get; set; }

        // FK to Automobil
        public int AutomobilId { get; set; }
        public virtual Automobil Automobil { get; set; }
        
        // FK to Mehanicar
        public int MehanicarId { get; set; }
        public virtual Mehanicar Mehanicar { get; set; }

        // 1-N relationship
        public virtual ICollection<NalogStavka> Stavke { get; set; }

        public ServisniNalog()
        {
            Stavke = new List<NalogStavka>();
            DatumOtvaranja = DateTime.Now;
            Status = StatusNaloga.Otvoren;
        }
    }
}
