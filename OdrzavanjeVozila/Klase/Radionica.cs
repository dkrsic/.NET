namespace OdrzavanjeVozila.Klase
{
    public class Radionica
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

   
        public List<Mehanicar> Mehanicari { get; set; }

        public Radionica()
        {
            Mehanicari = new List<Mehanicar>();
        }
    }
}
