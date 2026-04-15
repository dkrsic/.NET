using OdrzavanjeVozila.Tools;

namespace OdrzavanjeVozila.Klase
{
    public class Dio
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string KatalogBroj { get; set; }
        public string Proizvodac { get; set; }
        public decimal Cijena { get; set; }
        public KategorijaDijela Kategorija { get; set; }
        public int KolicinaNaSkladistu { get; set; }
        public string Opis { get; set; }

      
        public List<NalogStavka> Stavke { get; set; }

        public Dio()
        {
            Stavke = new List<NalogStavka>();
        }
    }
}
