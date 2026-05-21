using System.ComponentModel.DataAnnotations;

namespace OdrzavanjeVozila.Models
{
    public class RadionicaCreateModel
    {
        [Required(ErrorMessage = "Naziv je obvezan.")]
        [StringLength(150)]
        public string Naziv { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa je obvezna.")]
        [StringLength(200)]
        public string Adresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon je obvezan.")]
        [StringLength(50)]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email je obvezan.")]
        [EmailAddress(ErrorMessage = "Email nije u ispravnom formatu.")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;
    }

    public class RadionicaEditModel : RadionicaCreateModel
    {
        public int Id { get; set; }
    }
}