using System.ComponentModel.DataAnnotations;

namespace OdrzavanjeVozila.Klase
{
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumRegistracije { get; set; }

        // 1-N relationship
        public virtual ICollection<Automobil> Vozila { get; set; }

        public Korisnik()
        {
            Vozila = new List<Automobil>();
            DatumRegistracije = DateTime.Now;
        }

        public string PunoIme => $"{Ime} {Prezime}";
    }
}
