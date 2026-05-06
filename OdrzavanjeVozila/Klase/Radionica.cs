using System.ComponentModel.DataAnnotations;

namespace OdrzavanjeVozila.Klase
{
    public class Radionica
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        // 1-N relationship
        public virtual ICollection<Mehanicar> Mehanicari { get; set; }

        public Radionica()
        {
            Mehanicari = new List<Mehanicar>();
        }
    }
}
