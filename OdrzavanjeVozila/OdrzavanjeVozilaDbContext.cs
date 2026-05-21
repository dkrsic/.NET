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
                new Radionica { Id = 1, Naziv = "Radionica Zagreb", Adresa = "Ilica 100, Zagreb", Telefon = "+385 1 4829 615", Email = "kontakt.zagreb@radionica-mreza.hr" },
                new Radionica { Id = 2, Naziv = "Radionica Split", Adresa = "Marmontova 50, Split", Telefon = "+385 21 784 392", Email = "kontakt.split@radionica-mreza.hr" },
                new Radionica { Id = 3, Naziv = "Radionica Rijeka", Adresa = "Riva 12, Rijeka", Telefon = "+385 51 642 880", Email = "kontakt.rijeka@radionica-mreza.hr" }
            );

            // Seed Korisnici
            modelBuilder.Entity<Korisnik>().HasData(
                new Korisnik { Id = 1, Ime = "Marko", Prezime = "Horvat", Email = "marko.horvat@autosluzba.hr", Telefon = "+385 91 482 1163", Adresa = "Ilica 200, Zagreb", DatumRegistracije = new DateTime(2025, 1, 10) },
                new Korisnik { Id = 2, Ime = "Ana", Prezime = "Novak", Email = "ana.novak@autosluzba.hr", Telefon = "+385 91 637 2048", Adresa = "Gunduliceva 50, Split", DatumRegistracije = new DateTime(2025, 2, 14) },
                new Korisnik { Id = 3, Ime = "Ivan", Prezime = "Peric", Email = "ivan.peric@autosluzba.hr", Telefon = "+385 95 781 9044", Adresa = "Korzo 15, Rijeka", DatumRegistracije = new DateTime(2025, 3, 8) },
                new Korisnik { Id = 4, Ime = "Maja", Prezime = "Juric", Email = "maja.juric@autosluzba.hr", Telefon = "+385 98 520 7712", Adresa = "Dubrovacka 21, Zagreb", DatumRegistracije = new DateTime(2025, 4, 2) }
            );

            // Seed Mehanicari
            modelBuilder.Entity<Mehanicar>().HasData(
                new Mehanicar { Id = 1, Ime = "Petar", Prezime = "Kovac", Specijalizacija = "Mehanika", DatumZaposlenja = new DateTime(2020, 5, 1), SatnicaEUR = 25.00m, RadionicaId = 1 },
                new Mehanicar { Id = 2, Ime = "Jovan", Prezime = "Jovanovic", Specijalizacija = "Elektrika", DatumZaposlenja = new DateTime(2022, 3, 15), SatnicaEUR = 30.00m, RadionicaId = 1 },
                new Mehanicar { Id = 3, Ime = "Milan", Prezime = "Milanovic", Specijalizacija = "Mehanika", DatumZaposlenja = new DateTime(2023, 9, 1), SatnicaEUR = 28.00m, RadionicaId = 2 },
                new Mehanicar { Id = 4, Ime = "Stjepan", Prezime = "Boric", Specijalizacija = "Klimatizacija", DatumZaposlenja = new DateTime(2021, 11, 5), SatnicaEUR = 27.50m, RadionicaId = 3 },
                new Mehanicar { Id = 5, Ime = "Filip", Prezime = "Maric", Specijalizacija = "Dijagnostika", DatumZaposlenja = new DateTime(2024, 2, 20), SatnicaEUR = 32.00m, RadionicaId = 3 }
            );

            // Seed Automobili
            modelBuilder.Entity<Automobil>().HasData(
                new Automobil { Id = 1, Marka = "Volkswagen", Model = "Golf", Godiste = 2019, RegistracijskiBroj = "ZG-234-AB", BrojSasije = "WVWZZZ3CZ9E123456", TrenutnaKilometraza = 45000, VrstaPogona = OdrzavanjeVozila.Tools.VrstaPogona.Benzin, DatumPrvogServisa = new DateTime(2024, 6, 1), KorisnikId = 1 },
                new Automobil { Id = 2, Marka = "BMW", Model = "320", Godiste = 2020, RegistracijskiBroj = "ZG-567-CD", BrojSasije = "WBXYZ0000000000001", TrenutnaKilometraza = 32000, VrstaPogona = OdrzavanjeVozila.Tools.VrstaPogona.Dizel, DatumPrvogServisa = new DateTime(2023, 11, 20), KorisnikId = 2 },
                new Automobil { Id = 3, Marka = "Opel", Model = "Astra", Godiste = 2018, RegistracijskiBroj = "RI-345-EF", BrojSasije = "W0L0AHL48J1234567", TrenutnaKilometraza = 78000, VrstaPogona = OdrzavanjeVozila.Tools.VrstaPogona.Plin, DatumPrvogServisa = new DateTime(2024, 8, 12), KorisnikId = 3 },
                new Automobil { Id = 4, Marka = "Toyota", Model = "Corolla", Godiste = 2021, RegistracijskiBroj = "ZG-890-GH", BrojSasije = "JTNB43BE003123456", TrenutnaKilometraza = 21000, VrstaPogona = OdrzavanjeVozila.Tools.VrstaPogona.Hibridni, DatumPrvogServisa = new DateTime(2025, 1, 15), KorisnikId = 4 }
            );

            // Seed Dijelovi
            modelBuilder.Entity<Dio>().HasData(
                new Dio { Id = 1, Naziv = "Ulje motora", KatalogBroj = "OIL-001", Proizvodac = "Castrol", Cijena = 15.99m, Kategorija = OdrzavanjeVozila.Tools.KategorijaDijela.Motor, KolicinaNaSkladistu = 50, Opis = "Sintetsko motorno ulje" },
                new Dio { Id = 2, Naziv = "Kocione plocice", KatalogBroj = "BRK-001", Proizvodac = "Bosch", Cijena = 25.50m, Kategorija = OdrzavanjeVozila.Tools.KategorijaDijela.Kocnice, KolicinaNaSkladistu = 30, Opis = "Set prednjih kocionih plocica" },
                new Dio { Id = 3, Naziv = "Akumulator", KatalogBroj = "BAT-014", Proizvodac = "Varta", Cijena = 89.90m, Kategorija = OdrzavanjeVozila.Tools.KategorijaDijela.Elektrika, KolicinaNaSkladistu = 12, Opis = "Start-stop akumulator" },
                new Dio { Id = 4, Naziv = "Filter zraka", KatalogBroj = "FLT-022", Proizvodac = "Mann", Cijena = 12.75m, Kategorija = OdrzavanjeVozila.Tools.KategorijaDijela.Motor, KolicinaNaSkladistu = 18, Opis = "Motorni filter zraka" },
                new Dio { Id = 5, Naziv = "Gume 205/55 R16", KatalogBroj = "TIR-20555", Proizvodac = "Michelin", Cijena = 79.99m, Kategorija = OdrzavanjeVozila.Tools.KategorijaDijela.Gume, KolicinaNaSkladistu = 8, Opis = "Ljetni komplet guma" }
            );

            // Seed Servisni nalozi
            modelBuilder.Entity<ServisniNalog>().HasData(
                new ServisniNalog
                {
                    Id = 1,
                    DatumOtvaranja = new DateTime(2026, 1, 10),
                    DatumZatvaranja = new DateTime(2026, 1, 12),
                    SljedecaPreporucenaPregleda = new DateTime(2026, 7, 10),
                    OpisRadova = "Redovni servis i zamjena ulja",
                    UkupnaCijena = 96.97m,
                    Status = OdrzavanjeVozila.Tools.StatusNaloga.Zavrsen,
                    KilometrazaPrilikomServisa = 45250,
                    Napomena = "Vozilo uredno, preporucena kontrola za 6 mjeseci",
                    AutomobilId = 1,
                    MehanicarId = 1
                },
                new ServisniNalog
                {
                    Id = 2,
                    DatumOtvaranja = new DateTime(2026, 2, 3),
                    DatumZatvaranja = null,
                    SljedecaPreporucenaPregleda = new DateTime(2026, 8, 3),
                    OpisRadova = "Dijagnostika elektrike i zamjena akumulatora",
                    UkupnaCijena = 89.90m,
                    Status = OdrzavanjeVozila.Tools.StatusNaloga.UObradi,
                    KilometrazaPrilikomServisa = 32220,
                    Napomena = "Čeka potvrdu narudžbe dodatnog dijela",
                    AutomobilId = 2,
                    MehanicarId = 2
                },
                new ServisniNalog
                {
                    Id = 3,
                    DatumOtvaranja = new DateTime(2026, 3, 18),
                    DatumZatvaranja = new DateTime(2026, 3, 19),
                    SljedecaPreporucenaPregleda = new DateTime(2026, 9, 18),
                    OpisRadova = "Servis kočnica i zamjena plocica",
                    UkupnaCijena = 112.45m,
                    Status = OdrzavanjeVozila.Tools.StatusNaloga.Zavrsen,
                    KilometrazaPrilikomServisa = 78120,
                    Napomena = "Kočnice testirane i ispravne",
                    AutomobilId = 3,
                    MehanicarId = 3
                },
                new ServisniNalog
                {
                    Id = 4,
                    DatumOtvaranja = new DateTime(2026, 4, 7),
                    DatumZatvaranja = null,
                    SljedecaPreporucenaPregleda = null,
                    OpisRadova = "Zamjena guma i sezonski pregled",
                    UkupnaCijena = 79.99m,
                    Status = OdrzavanjeVozila.Tools.StatusNaloga.Otvoren,
                    KilometrazaPrilikomServisa = 21400,
                    Napomena = "Čeka dolazak vozila u radionicu",
                    AutomobilId = 4,
                    MehanicarId = 4
                }
            );

            // Seed stavke naloga
            modelBuilder.Entity<NalogStavka>().HasData(
                new NalogStavka { Id = 1, Kolicina = 1, CijenaKomad = 15.99m, Napomena = "Motorno ulje 5W-30", NalogId = 1, DioId = 1 },
                new NalogStavka { Id = 2, Kolicina = 4, CijenaKomad = 25.50m, Napomena = "Prednji set kočionih pločica", NalogId = 1, DioId = 2 },
                new NalogStavka { Id = 3, Kolicina = 1, CijenaKomad = 89.90m, Napomena = "Start-stop akumulator", NalogId = 2, DioId = 3 },
                new NalogStavka { Id = 4, Kolicina = 2, CijenaKomad = 12.75m, Napomena = "Filteri za zrak", NalogId = 3, DioId = 4 },
                new NalogStavka { Id = 5, Kolicina = 4, CijenaKomad = 79.99m, Napomena = "Set ljetnih guma", NalogId = 4, DioId = 5 }
            );
        }
    }
}
