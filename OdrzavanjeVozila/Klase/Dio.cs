using OdrzavanjeVozila.Tools;
using System.ComponentModel.DataAnnotations;

namespace OdrzavanjeVozila.Klase
{
    public class Dio
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string KatalogBroj { get; set; }
        public string Proizvodac { get; set; }
        public decimal Cijena { get; set; }
        public KategorijaDijela Kategorija { get; set; }
        public int KolicinaNaSkladistu { get; set; }
        public string Opis { get; set; }

        // 1-N relationship
        public virtual ICollection<NalogStavka> Stavke { get; set; }

        public Dio()
        {
            Stavke = new List<NalogStavka>();
        }
    }
}
