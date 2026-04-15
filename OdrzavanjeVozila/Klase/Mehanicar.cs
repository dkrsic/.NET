namespace OdrzavanjeVozila.Klase
{
    public class Mehanicar
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Specijalizacija { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public decimal SatnicaEUR { get; set; }

    
        public int RadionicaId { get; set; }
        public Radionica Radionica { get; set; }

        public List<ServisniNalog> Nalozi { get; set; }

        public Mehanicar()
        {
            Nalozi = new List<ServisniNalog>();
        }

        public string PunoIme => $"{Ime} {Prezime}";
    }
}
