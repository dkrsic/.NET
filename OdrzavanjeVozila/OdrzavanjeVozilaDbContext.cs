using Microsoft.EntityFrameworkCore;
using OdrzavanjeVozila.Klase;

namespace OdrzavanjeVozila
{
    public class OdrzavanjeVozilaDbContext : DbContext
    {
        public OdrzavanjeVozilaDbContext() { }

        public OdrzavanjeVozilaDbContext(DbContextOptions<OdrzavanjeVozilaDbContext> options) 
            : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Automobil> Automobili { get; set; }
        public DbSet<Radionica> Radionice { get; set; }
        public DbSet<Mehanicar> Mehanicari { get; set; }
        public DbSet<ServisniNalog> ServisniNalozi { get; set; }
        public DbSet<NalogStavka> NalogStavke { get; set; }
        public DbSet<Dio> Dijelovi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Default configuration if not provided via DI
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OdrzavanjeVozila;Trusted_Connection=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships explicitly if needed
            modelBuilder.Entity<Automobil>()
                .HasOne(a => a.Korisnik)
                .WithMany(k => k.Vozila)
                .HasForeignKey(a => a.KorisnikId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServisniNalog>()
                .HasOne(sn => sn.Automobil)
                .WithMany(a => a.ServisniNalozi)
                .HasForeignKey(sn => sn.AutomobilId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServisniNalog>()
                .HasOne(sn => sn.Mehanicar)
                .WithMany(m => m.Nalozi)
                .HasForeignKey(sn => sn.MehanicarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mehanicar>()
                .HasOne(m => m.Radionica)
                .WithMany(r => r.Mehanicari)
                .HasForeignKey(m => m.RadionicaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NalogStavka>()
                .HasOne(ns => ns.Nalog)
                .WithMany(sn => sn.Stavke)
                .HasForeignKey(ns => ns.NalogId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NalogStavka>()
                .HasOne(ns => ns.Dio)
                .WithMany(d => d.Stavke)
                .HasForeignKey(ns => ns.DioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Dio>()
                .Property(d => d.Cijena)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Mehanicar>()
                .Property(m => m.SatnicaEUR)
                .HasPrecision(18, 2);

            modelBuilder.Entity<NalogStavka>()
                .Property(ns => ns.CijenaKomad)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ServisniNalog>()
                .Property(sn => sn.UkupnaCijena)
                .HasPrecision(18, 2);

            // Seed initial data
            SeedInitialData(modelBuilder);
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            // Seed Radionice
            modelBuilder.Entity<Radionica>().HasData(
                new Radionica { Id = 1, Naziv = "Radionica Zagreb", Adresa = "Ilica 100, Zagreb", Telefon = "01-1234567", Email = "info@radionica-zg.hr" },
                new Radionica { Id = 2, Naziv = "Radionica Split", Adresa = "Marmontova 50, Split", Telefon = "021-1234567", Email = "info@radionica-split.hr" }
            );

            // Seed Korisnici
            modelBuilder.Entity<Korisnik>().HasData(
                new Korisnik { Id = 1, Ime = "Marko", Prezime = "Horvat", Email = "marko@mail.com", Telefon = "099-1111111", Adresa = "Ilica 200, Zagreb", DatumRegistracije = new DateTime(2025, 1, 10) },
                new Korisnik { Id = 2, Ime = "Ana", Prezime = "Novak", Email = "ana@mail.com", Telefon = "099-2222222", Adresa = "Gunduliceva 50, Split", DatumRegistracije = new DateTime(2025, 2, 14) }
            );

            // Seed Mehanicari
            modelBuilder.Entity<Mehanicar>().HasData(
                new Mehanicar { Id = 1, Ime = "Petar", Prezime = "Kovac", Specijalizacija = "Mehanika", DatumZaposlenja = new DateTime(2020, 5, 1), SatnicaEUR = 25.00m, RadionicaId = 1 },
                new Mehanicar { Id = 2, Ime = "Jovan", Prezime = "Jovanovic", Specijalizacija = "Elektrika", DatumZaposlenja = new DateTime(2022, 3, 15), SatnicaEUR = 30.00m, RadionicaId = 1 },
                new Mehanicar { Id = 3, Ime = "Milan", Prezime = "Milanovic", Specijalizacija = "Mehanika", DatumZaposlenja = new DateTime(2023, 9, 1), SatnicaEUR = 28.00m, RadionicaId = 2 }
            );

            // Seed Automobili
            modelBuilder.Entity<Automobil>().HasData(
                new Automobil { Id = 1, Marka = "Volkswagen", Model = "Golf", Godiste = 2019, RegistracijskiBroj = "ZG-234-AB", BrojSasije = "WVWZZZ3CZ9E123456", TrenutnaKilometraza = 45000, VrstaPogona = OdrzavanjeVozila.Tools.VrstaPogona.Benzin, DatumPrvogServisa = new DateTime(2024, 6, 1), KorisnikId = 1 },
                new Automobil { Id = 2, Marka = "BMW", Model = "320", Godiste = 2020, RegistracijskiBroj = "ZG-567-CD", BrojSasije = "WBXYZ0000000000001", TrenutnaKilometraza = 32000, VrstaPogona = OdrzavanjeVozila.Tools.VrstaPogona.Dizel, DatumPrvogServisa = new DateTime(2023, 11, 20), KorisnikId = 2 }
            );

            // Seed Dijelovi
            modelBuilder.Entity<Dio>().HasData(
                new Dio { Id = 1, Naziv = "Ulje motora", KatalogBroj = "OIL-001", Proizvodac = "Castrol", Cijena = 15.99m, Kategorija = OdrzavanjeVozila.Tools.KategorijaDijela.Motor, KolicinaNaSkladistu = 50, Opis = "Sintetsko motorno ulje" },
                new Dio { Id = 2, Naziv = "Kocione plocice", KatalogBroj = "BRK-001", Proizvodac = "Bosch", Cijena = 25.50m, Kategorija = OdrzavanjeVozila.Tools.KategorijaDijela.Kocnice, KolicinaNaSkladistu = 30, Opis = "Set prednjih kocionih plocica" }
            );
        }
    }
}
