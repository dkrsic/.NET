using System.ComponentModel.DataAnnotations;

namespace OdrzavanjeVozila.Models
{
    public class KorisnikCreateModel
    {
        [Required(ErrorMessage = "Ime je obvezno.")]
        [StringLength(50, ErrorMessage = "Ime smije imati najviše 50 znakova.")]
        public string Ime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prezime je obvezno.")]
        [StringLength(50, ErrorMessage = "Prezime smije imati najviše 50 znakova.")]
        public string Prezime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email je obvezan.")]
        [EmailAddress(ErrorMessage = "Email nije u ispravnom formatu.")]
        [StringLength(100, ErrorMessage = "Email smije imati najviše 100 znakova.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon je obvezan.")]
        [StringLength(30, ErrorMessage = "Telefon smije imati najviše 30 znakova.")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa je obvezna.")]
        [StringLength(200, ErrorMessage = "Adresa smije imati najviše 200 znakova.")]
        public string Adresa { get; set; } = string.Empty;

        public string PunoIme => $"{Ime} {Prezime}";
    }
}