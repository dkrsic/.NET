namespace OdrzavanjeVozila.Klase
{
    public class NalogStavka
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public decimal CijenaKomad { get; set; }
        public string Napomena { get; set; }

     
        public int NalogId { get; set; }
        public ServisniNalog Nalog { get; set; }

        public int DioId { get; set; }
        public Dio Dio { get; set; }

        public decimal UkupnaCijenaStavke => Kolicina * CijenaKomad;
    }
}
