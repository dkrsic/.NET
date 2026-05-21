using System.ComponentModel.DataAnnotations;
using OdrzavanjeVozila.Tools;

namespace OdrzavanjeVozila.Models
{
    public class DioCreateModel
    {
        [Required(ErrorMessage = "Naziv je obvezan.")]
        [StringLength(150)]
        public string Naziv { get; set; } = string.Empty;

        [Required(ErrorMessage = "Katalog broj je obvezan.")]
        [StringLength(100)]
        public string KatalogBroj { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proizvođač je obvezan.")]
        [StringLength(150)]
        public string Proizvodac { get; set; } = string.Empty;

        [Range(0, 9999999.99, ErrorMessage = "Cijena mora biti 0 ili veća.")]
        public decimal Cijena { get; set; }

        [Required(ErrorMessage = "Kategorija je obvezna.")]
        public KategorijaDijela Kategorija { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Količina mora biti 0 ili veća.")]
        public int KolicinaNaSkladistu { get; set; }

        [Required(ErrorMessage = "Opis je obvezan.")]
        [StringLength(1000)]
        public string Opis { get; set; } = string.Empty;
    }

    public class DioEditModel : DioCreateModel
    {
        public int Id { get; set; }
    }
}