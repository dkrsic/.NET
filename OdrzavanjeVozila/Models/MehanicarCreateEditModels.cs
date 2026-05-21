using System.ComponentModel.DataAnnotations;

namespace OdrzavanjeVozila.Models
{
    public class MehanicarCreateModel
    {
        [Required(ErrorMessage = "Ime je obvezno.")]
        [StringLength(100)]
        public string Ime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prezime je obvezno.")]
        [StringLength(100)]
        public string Prezime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specijalizacija je obvezna.")]
        [StringLength(150)]
        public string Specijalizacija { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DatumZaposlenja { get; set; } = DateTime.Today;

        [Range(0, 999999.99, ErrorMessage = "Satnica mora biti 0 ili veća.")]
        public decimal SatnicaEUR { get; set; }

        [Required(ErrorMessage = "Radionica je obvezna.")]
        public int? RadionicaId { get; set; }
    }

    public class MehanicarEditModel : MehanicarCreateModel
    {
        public int Id { get; set; }
    }
}