using System.ComponentModel.DataAnnotations;
using OdrzavanjeVozila.Tools;

namespace OdrzavanjeVozila.Models
{
    public class AutomobilCreateModel
    {
        [Required(ErrorMessage = "Marka je obvezna.")]
        [StringLength(100)]
        public string Marka { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model je obvezan.")]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;

        [Range(1950, 2100, ErrorMessage = "Godište mora biti između 1950 i 2100.")]
        public int Godiste { get; set; }

        [Required(ErrorMessage = "Registracijski broj je obvezan.")]
        [StringLength(50)]
        public string RegistracijskiBroj { get; set; } = string.Empty;

        [Required(ErrorMessage = "Broj šasije je obvezan.")]
        [StringLength(100)]
        public string BrojSasije { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Kilometraža mora biti 0 ili veća.")]
        public int TrenutnaKilometraza { get; set; }

        [Required(ErrorMessage = "Pogon je obvezan.")]
        public VrstaPogona VrstaPogona { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumPrvogServisa { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vlasnik je obvezan.")]
        public int? KorisnikId { get; set; }
    }

    public class AutomobilEditModel : AutomobilCreateModel
    {
        public int Id { get; set; }
    }
}