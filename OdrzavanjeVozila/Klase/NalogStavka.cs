using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdrzavanjeVozila.Klase
{
    public class NalogStavka
    {
        [Key]
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public decimal CijenaKomad { get; set; }
        public string Napomena { get; set; }

        // FK to ServisniNalog
        public int NalogId { get; set; }
        public virtual ServisniNalog Nalog { get; set; }

        // FK to Dio
        public int DioId { get; set; }
        public virtual Dio Dio { get; set; }

        public decimal UkupnaCijenaStavke => Kolicina * CijenaKomad;
    }
}
