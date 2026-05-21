using System.ComponentModel.DataAnnotations;
using OdrzavanjeVozila.Tools;

namespace OdrzavanjeVozila.Models
{
    public class ServisniNalogCreateModel
    {
        [Required(ErrorMessage = "Datum otvaranja je obvezan.")]
        [DataType(DataType.Date)]
        public DateTime DatumOtvaranja { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime? DatumZatvaranja { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SljedecaPreporucenaPregleda { get; set; }

        [Required(ErrorMessage = "Opis radova je obvezan.")]
        [StringLength(1000, ErrorMessage = "Opis radova smije imati najviše 1000 znakova.")]
        public string OpisRadova { get; set; } = string.Empty;

        [Range(0.01, 9999999.99, ErrorMessage = "Ukupna cijena mora biti veća od 0.")]
        public decimal UkupnaCijena { get; set; }

        [Required(ErrorMessage = "Status je obvezan.")]
        public StatusNaloga Status { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Kilometraža mora biti 0 ili veća.")]
        public int KilometrazaPrilikomServisa { get; set; }

        [StringLength(1000, ErrorMessage = "Napomena smije imati najviše 1000 znakova.")]
        public string? Napomena { get; set; }

        [Required(ErrorMessage = "Automobil je obvezan.")]
        public int? AutomobilId { get; set; }

        [Required(ErrorMessage = "Mehaničar je obvezan.")]
        public int? MehanicarId { get; set; }
    }

    public class ServisniNalogEditModel : ServisniNalogCreateModel
    {
        public int Id { get; set; }
    }
}