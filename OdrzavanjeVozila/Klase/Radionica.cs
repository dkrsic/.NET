using System.ComponentModel.DataAnnotations;

namespace OdrzavanjeVozila.Klase
{
    public class Radionica
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        // 1-N relationship
        public virtual ICollection<Mehanicar> Mehanicari { get; set; }

        public Radionica()
        {
            Mehanicari = new List<Mehanicar>();
        }

        // Soft-delete timestamp: null when active
        public DateTime? DeletedAt { get; set; }
    }
}
