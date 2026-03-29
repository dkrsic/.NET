using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vjezba.Model.Klase;
using Vjezba.Model.Tools;




class Program
{
    static void Main(string[] args)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("hr-HR");
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // ──────────────────────────────────────────
        //  1. PUNJENJE PODATAKA
        // ──────────────────────────────────────────

        // -- Radionice --
        var radionica1 = new Radionica { Id = 1, Naziv = "AutoServis Centar Zagreb", Adresa = "Ilica 120, Zagreb", Telefon = "01/555-1234", Email = "info@ascz.hr" };
        var radionica2 = new Radionica { Id = 2, Naziv = "Moto Fix Split", Adresa = "Kralja Tomislava 5, Split", Telefon = "021/333-9900", Email = "servis@motofix.hr" };
        var radionica3 = new Radionica { Id = 3, Naziv = "Euro Auto Rijeka", Adresa = "Fiorello la Guardia 8, Rijeka", Telefon = "051/222-0011", Email = "euro@autorijeka.hr" };

        // -- Mehaničari --
        var m1 = new Mehanicar { Id = 1, Ime = "Ivan", Prezime = "Horvat", Specijalizacija = "Motor i mjenjač", DatumZaposlenja = new DateTime(2018, 3, 15), SatnicaEUR = 18.50m, RadionicaId = 1, Radionica = radionica1 };
        var m2 = new Mehanicar { Id = 2, Ime = "Maja", Prezime = "Kovač", Specijalizacija = "Elektrika i dijag", DatumZaposlenja = new DateTime(2020, 6, 1), SatnicaEUR = 20.00m, RadionicaId = 1, Radionica = radionica1 };
        var m3 = new Mehanicar { Id = 3, Ime = "Ante", Prezime = "Marić", Specijalizacija = "Kočnice i ovjes", DatumZaposlenja = new DateTime(2015, 9, 20), SatnicaEUR = 17.00m, RadionicaId = 2, Radionica = radionica2 };
        var m4 = new Mehanicar { Id = 4, Ime = "Petra", Prezime = "Babić", Specijalizacija = "Klimatizacija", DatumZaposlenja = new DateTime(2022, 1, 10), SatnicaEUR = 16.00m, RadionicaId = 3, Radionica = radionica3 };

        radionica1.Mehanicari.AddRange(new[] { m1, m2 });
        radionica2.Mehanicari.Add(m3);
        radionica3.Mehanicari.Add(m4);

        // -- Dijelovi --
        var d1 = new Dio { Id = 1, Naziv = "Uljni filter", KatalogBroj = "OIL-001", Proizvodac = "Mann Filter", Cijena = 12.50m, Kategorija = KategorijaDijela.Motor, KolicinaNaSkladistu = 30 };
        var d2 = new Dio { Id = 2, Naziv = "Motorno ulje 5W-40", KatalogBroj = "OIL-002", Proizvodac = "Castrol", Cijena = 8.90m, Kategorija = KategorijaDijela.Motor, KolicinaNaSkladistu = 50 };
        var d3 = new Dio { Id = 3, Naziv = "Prednje kočione pločice", KatalogBroj = "BRK-101", Proizvodac = "Brembo", Cijena = 45.00m, Kategorija = KategorijaDijela.Kocnice, KolicinaNaSkladistu = 20 };
        var d4 = new Dio { Id = 4, Naziv = "Disk kočnica prednji", KatalogBroj = "BRK-102", Proizvodac = "Brembo", Cijena = 89.00m, Kategorija = KategorijaDijela.Kocnice, KolicinaNaSkladistu = 10 };
        var d5 = new Dio { Id = 5, Naziv = "Svjećice NGK", KatalogBroj = "ENG-210", Proizvodac = "NGK", Cijena = 7.50m, Kategorija = KategorijaDijela.Motor, KolicinaNaSkladistu = 40 };
        var d6 = new Dio { Id = 6, Naziv = "Amortizer prednji", KatalogBroj = "SUS-300", Proizvodac = "KYB", Cijena = 120.00m, Kategorija = KategorijaDijela.Ovjes, KolicinaNaSkladistu = 8 };
        var d7 = new Dio { Id = 7, Naziv = "Filtar zraka", KatalogBroj = "AIR-050", Proizvodac = "Mann Filter", Cijena = 18.00m, Kategorija = KategorijaDijela.Motor, KolicinaNaSkladistu = 25 };

        var sviDijelovi = new List<Dio> { d1, d2, d3, d4, d5, d6, d7 };

        // -- Korisnici --
        var k1 = new Korisnik { Id = 1, Ime = "Tomislav", Prezime = "Jurić", Email = "tjuric@gmail.com", Telefon = "091/111-2222", Adresa = "Savska 45, Zagreb", DatumRegistracije = new DateTime(2021, 1, 10) };
        var k2 = new Korisnik { Id = 2, Ime = "Ana", Prezime = "Perić", Email = "ana.peric@net.hr", Telefon = "095/333-4444", Adresa = "Marmontova 3, Split", DatumRegistracije = new DateTime(2022, 5, 20) };
        var k3 = new Korisnik { Id = 3, Ime = "Marko", Prezime = "Šimić", Email = "marko.simic@xnet.hr", Telefon = "099/777-8888", Adresa = "Korzo 12, Rijeka", DatumRegistracije = new DateTime(2020, 9, 5) };

        // -- Vozila --
        var v1 = new Automobil { Id = 1, Marka = "Volkswagen", Model = "Golf 7", Godiste = 2017, RegistracijskiBroj = "ZG-123-AA", BrojSasije = "WVWZZZ1KZ1W123456", TrenutnaKilometraza = 120000, VrstaPogona = VrstaPogona.Dizel, DatumPrvogServisa = new DateTime(2017, 9, 1), KorisnikId = 1, Korisnik = k1 };
        var v2 = new Automobil { Id = 2, Marka = "Škoda", Model = "Octavia", Godiste = 2019, RegistracijskiBroj = "ZG-456-BB", BrojSasije = "TMBZZZ1UZK1654321", TrenutnaKilometraza = 85000, VrstaPogona = VrstaPogona.Benzin, DatumPrvogServisa = new DateTime(2019, 3, 15), KorisnikId = 1, Korisnik = k1 };
        var v3 = new Automobil { Id = 3, Marka = "Toyota", Model = "Yaris", Godiste = 2021, RegistracijskiBroj = "ST-789-CC", BrojSasije = "JTDKGNEC20J012345", TrenutnaKilometraza = 42000, VrstaPogona = VrstaPogona.Hibridni, DatumPrvogServisa = new DateTime(2021, 6, 20), KorisnikId = 2, Korisnik = k2 };
        var v4 = new Automobil { Id = 4, Marka = "Renault", Model = "Megane", Godiste = 2016, RegistracijskiBroj = "RI-321-DD", BrojSasije = "VF1KFG00056789012", TrenutnaKilometraza = 175000, VrstaPogona = VrstaPogona.Dizel, DatumPrvogServisa = new DateTime(2016, 4, 10), KorisnikId = 3, Korisnik = k3 };
        var v5 = new Automobil { Id = 5, Marka = "BMW", Model = "320d", Godiste = 2020, RegistracijskiBroj = "ZG-654-EE", BrojSasije = "WBA8E9102NJ123789", TrenutnaKilometraza = 60000, VrstaPogona = VrstaPogona.Dizel, DatumPrvogServisa = new DateTime(2020, 11, 5), KorisnikId = 3, Korisnik = k3 };

        k1.Vozila.AddRange(new[] { v1, v2 });
        k2.Vozila.Add(v3);
        k3.Vozila.AddRange(new[] { v4, v5 });

        // -- Servisni nalozi + stavke --

        // Golf 7 – zamjena ulja i filtera
        var n1 = new ServisniNalog
        {
            Id = 1,
            AutomobilId = 1,
            Automobil = v1,
            MehanicarId = 1,
            Mehanicar = m1,
            DatumOtvaranja = new DateTime(2024, 2, 10),
            DatumZatvaranja = new DateTime(2024, 2, 10),
            SljedecaPreporucenaPregleda = new DateTime(2024, 8, 10),
            OpisRadova = "Redovni servis – zamjena motornog ulja i filtera",
            Status = StatusNaloga.Zavrsen,
            KilometrazaPrilikomServisa = 110000,
            Napomena = "Preporuča se zamjena svjećica na sljedećem servisu"
        };
        var s1a = new NalogStavka { Id = 1, NalogId = 1, Nalog = n1, DioId = 1, Dio = d1, Kolicina = 1, CijenaKomad = 12.50m };
        var s1b = new NalogStavka { Id = 2, NalogId = 1, Nalog = n1, DioId = 2, Dio = d2, Kolicina = 5, CijenaKomad = 8.90m };
        n1.Stavke.AddRange(new[] { s1a, s1b });
        n1.UkupnaCijena = n1.Stavke.Sum(s => s.UkupnaCijenaStavke) + 30m; // radna snaga
        d1.Stavke.Add(s1a); d2.Stavke.Add(s1b);

        // Golf 7 – kočnice
        var n2 = new ServisniNalog
        {
            Id = 2,
            AutomobilId = 1,
            Automobil = v1,
            MehanicarId = 2,
            Mehanicar = m2,
            DatumOtvaranja = new DateTime(2024, 6, 15),
            DatumZatvaranja = new DateTime(2024, 6, 15),
            SljedecaPreporucenaPregleda = new DateTime(2025, 6, 15),
            OpisRadova = "Zamjena prednjih kočnih pločica i diskova",
            Status = StatusNaloga.Zavrsen,
            KilometrazaPrilikomServisa = 118000,
            Napomena = ""
        };
        var s2a = new NalogStavka { Id = 3, NalogId = 2, Nalog = n2, DioId = 3, Dio = d3, Kolicina = 1, CijenaKomad = 45.00m };
        var s2b = new NalogStavka { Id = 4, NalogId = 2, Nalog = n2, DioId = 4, Dio = d4, Kolicina = 2, CijenaKomad = 89.00m };
        n2.Stavke.AddRange(new[] { s2a, s2b });
        n2.UkupnaCijena = n2.Stavke.Sum(s => s.UkupnaCijenaStavke) + 80m;
        d3.Stavke.Add(s2a); d4.Stavke.Add(s2b);

        // Octavia – redovni servis
        var n3 = new ServisniNalog
        {
            Id = 3,
            AutomobilId = 2,
            Automobil = v2,
            MehanicarId = 1,
            Mehanicar = m1,
            DatumOtvaranja = new DateTime(2024, 3, 5),
            DatumZatvaranja = new DateTime(2024, 3, 5),
            SljedecaPreporucenaPregleda = new DateTime(2024, 9, 5),
            OpisRadova = "Redovni servis – ulje, filter zraka, svjećice",
            Status = StatusNaloga.Zavrsen,
            KilometrazaPrilikomServisa = 80000,
            Napomena = ""
        };
        var s3a = new NalogStavka { Id = 5, NalogId = 3, Nalog = n3, DioId = 1, Dio = d1, Kolicina = 1, CijenaKomad = 12.50m };
        var s3b = new NalogStavka { Id = 6, NalogId = 3, Nalog = n3, DioId = 7, Dio = d7, Kolicina = 1, CijenaKomad = 18.00m };
        var s3c = new NalogStavka { Id = 7, NalogId = 3, Nalog = n3, DioId = 5, Dio = d5, Kolicina = 4, CijenaKomad = 7.50m };
        n3.Stavke.AddRange(new[] { s3a, s3b, s3c });
        n3.UkupnaCijena = n3.Stavke.Sum(s => s.UkupnaCijenaStavke) + 50m;
        d1.Stavke.Add(s3a); d7.Stavke.Add(s3b); d5.Stavke.Add(s3c);

        // Toyota Yaris – otvoreni nalog (u obradi)
        var n4 = new ServisniNalog
        {
            Id = 4,
            AutomobilId = 3,
            Automobil = v3,
            MehanicarId = 3,
            Mehanicar = m3,
            DatumOtvaranja = new DateTime(2025, 1, 20),
            OpisRadova = "Provjera ovjesnog sustava – šum pri vožnji",
            Status = StatusNaloga.UObradi,
            KilometrazaPrilikomServisa = 41500,
            Napomena = "Čeka se dio"
        };
        var s4a = new NalogStavka { Id = 8, NalogId = 4, Nalog = n4, DioId = 6, Dio = d6, Kolicina = 2, CijenaKomad = 120.00m };
        n4.Stavke.Add(s4a);
        n4.UkupnaCijena = 0; // još nije završen
        d6.Stavke.Add(s4a);

        // Renault Megane – servis
        var n5 = new ServisniNalog
        {
            Id = 5,
            AutomobilId = 4,
            Automobil = v4,
            MehanicarId = 4,
            Mehanicar = m4,
            DatumOtvaranja = new DateTime(2024, 11, 8),
            DatumZatvaranja = new DateTime(2024, 11, 8),
            SljedecaPreporucenaPregleda = new DateTime(2025, 5, 8),
            OpisRadova = "Servis klime i zamjena ulja",
            Status = StatusNaloga.Zavrsen,
            KilometrazaPrilikomServisa = 170000,
            Napomena = "Klima dopunjena rashladnim sredstvom"
        };
        var s5a = new NalogStavka { Id = 9, NalogId = 5, Nalog = n5, DioId = 1, Dio = d1, Kolicina = 1, CijenaKomad = 12.50m };
        var s5b = new NalogStavka { Id = 10, NalogId = 5, Nalog = n5, DioId = 2, Dio = d2, Kolicina = 4, CijenaKomad = 8.90m };
        n5.Stavke.AddRange(new[] { s5a, s5b });
        n5.UkupnaCijena = n5.Stavke.Sum(s => s.UkupnaCijenaStavke) + 120m; // klima = skuplja usluga
        d1.Stavke.Add(s5a); d2.Stavke.Add(s5b);

        // BMW 320d – otvoreni nalog
        var n6 = new ServisniNalog
        {
            Id = 6,
            AutomobilId = 5,
            Automobil = v5,
            MehanicarId = 1,
            Mehanicar = m1,
            DatumOtvaranja = new DateTime(2025, 2, 3),
            OpisRadova = "Redovni servis – ulje, filter, dijagnostika",
            Status = StatusNaloga.Otvoren,
            KilometrazaPrilikomServisa = 59800,
            Napomena = ""
        };
        n6.UkupnaCijena = 0;

        // Dodaj naloge vozilima
        v1.ServisniNalozi.AddRange(new[] { n1, n2 });
        v2.ServisniNalozi.Add(n3);
        v3.ServisniNalozi.Add(n4);
        v4.ServisniNalozi.Add(n5);
        v5.ServisniNalozi.Add(n6);

        // Dodaj naloge mehaničarima
        m1.Nalozi.AddRange(new[] { n1, n3, n6 });
        m2.Nalozi.Add(n2);
        m3.Nalozi.Add(n4);
        m4.Nalozi.Add(n5);

        // Kolekcije za LINQ upite
        var sviKorisnici = new List<Korisnik> { k1, k2, k3 };
        var svaVozila = new List<Automobil> { v1, v2, v3, v4, v5 };
        var sviNalozi = new List<ServisniNalog> { n1, n2, n3, n4, n5, n6 };
        var sviMehanicari = new List<Mehanicar> { m1, m2, m3, m4 };
        var sveRadionice = new List<Radionica> { radionica1, radionica2, radionica3 };

        // ──────────────────────────────────────────
        //  2. LINQ UPITI
        // ──────────────────────────────────────────

        // ── Upit 1: Sva vozila s dizel pogonom ────────────────────────────────────
        Console.WriteLine("=== 1. Vozila s dizel pogonom ===");
        var dizelVozila = svaVozila
            .Where(v => v.VrstaPogona == VrstaPogona.Dizel)
            .OrderBy(v => v.Marka)
            .ToList();
        dizelVozila.ForEach(v => Console.WriteLine($"  {v.Naziv} – {v.RegistracijskiBroj}"));

        // ── Upit 2: Svi otvoreni/u obradi nalozi ──────────────────────────────────
        Console.WriteLine("\n=== 2. Aktivni nalozi (Otvoren ili UObradi) ===");
        var aktivniNalozi = sviNalozi
            .Where(n => n.Status == StatusNaloga.Otvoren || n.Status == StatusNaloga.UObradi)
            .OrderBy(n => n.DatumOtvaranja)
            .ToList();
        aktivniNalozi.ForEach(n => Console.WriteLine($"  Nalog #{n.Id} – {n.Automobil.Naziv} – {n.Status}"));

        // ── Upit 3: Ukupna potrošnja po vozilu (samo završeni nalozi) ─────────────
        Console.WriteLine("\n=== 3. Ukupna potrošnja po vozilu ===");
        var potrosnja = svaVozila
            .Select(v => new
            {
                Automobil = v.Naziv,
                Ukupno = v.ServisniNalozi
                    .Where(n => n.Status == StatusNaloga.Zavrsen)
                    .Sum(n => n.UkupnaCijena)
            })
            .OrderByDescending(x => x.Ukupno)
            .ToList();
        potrosnja.ForEach(x => Console.WriteLine($"  {x.Automobil}: {x.Ukupno:C}"));

        // ── Upit 4: Vozila kojoj je preporučeni pregled prošao ili dolazi uka 30 dana ──
        Console.WriteLine("\n=== 4. Vozila kojima treba servis ===");
        var granica = DateTime.Now.AddDays(30);
        var trebajuServis = sviNalozi
            .Where(n => n.SljedecaPreporucenaPregleda.HasValue
                        && n.SljedecaPreporucenaPregleda.Value <= granica
                        && n.Status == StatusNaloga.Zavrsen)
            .OrderBy(n => n.SljedecaPreporucenaPregleda)
            .Select(n => new
            {
                Automobil = n.Automobil.Naziv,
                Preporuka = n.SljedecaPreporucenaPregleda.Value.ToShortDateString()
            })
            .ToList();
        trebajuServis.ForEach(x => Console.WriteLine($"  {x.Automobil} – preporučeni pregled: {x.Preporuka}"));

        // ── Upit 5: Top mehaničar po broju završenih naloga ───────────────────────
        Console.WriteLine("\n=== 5. Mehaničari po broju završenih naloga ===");
        var topMek = sviMehanicari
            .Select(m => new
            {
                Mehanicar = m.PunoIme,
                ZavrseniNalozi = m.Nalozi.Count(n => n.Status == StatusNaloga.Zavrsen)
            })
            .OrderByDescending(x => x.ZavrseniNalozi)
            .ToList();
        topMek.ForEach(x => Console.WriteLine($"  {x.Mehanicar}: {x.ZavrseniNalozi} završenih naloga"));

        // ── Upit 6: Najčešće korišteni dijelovi (po ukupnoj iskorištenoj količini) ─
        Console.WriteLine("\n=== 6. Najkorišteniji dijelovi ===");
        var najDijelovi = sviDijelovi
            .Select(d => new
            {
                Naziv = d.Naziv,
                UkupnoKolicina = d.Stavke.Sum(s => s.Kolicina),
                Prihod = d.Stavke.Sum(s => s.UkupnaCijenaStavke)
            })
            .Where(d => d.UkupnoKolicina > 0)
            .OrderByDescending(d => d.UkupnoKolicina)
            .ToList();
        najDijelovi.ForEach(d => Console.WriteLine($"  {d.Naziv}: {d.UkupnoKolicina} kom / prihod {d.Prihod:C}"));

        // ── Upit 7: Korisnici s više od jednog vozila ─────────────────────────────
        Console.WriteLine("\n=== 7. Korisnici s više od jednog vozila ===");
        var visevlasnički = sviKorisnici
            .Where(k => k.Vozila.Count > 1)
            .Select(k => new
            {
                Korisnik = k.PunoIme,
                BrojVozila = k.Vozila.Count,
                Vozila = string.Join(", ", k.Vozila.Select(v => v.Naziv))
            })
            .ToList();
        visevlasnički.ForEach(k => Console.WriteLine($"  {k.Korisnik} ({k.BrojVozila} vozila): {k.Vozila}"));

        // ── Upit 8: Prosječna cijena servisa po radionici ─────────────────────────
        Console.WriteLine("\n=== 8. Prosječna cijena završenog servisa po radionici ===");
        var prosjecCijene = sveRadionice
            .Select(r => new
            {
                Radionica = r.Naziv,
                ProsjecnaCijena = r.Mehanicari
                    .SelectMany(m => m.Nalozi)
                    .Where(n => n.Status == StatusNaloga.Zavrsen && n.UkupnaCijena > 0)
                    .Select(n => n.UkupnaCijena)
                    .DefaultIfEmpty(0)
                    .Average()
            })
            .OrderByDescending(x => x.ProsjecnaCijena)
            .ToList();
        prosjecCijene.ForEach(x => Console.WriteLine($"  {x.Radionica}: {x.ProsjecnaCijena:C}"));

        // ── Upit 9: Vozila starija od 7 godina s visokom kilometražom (>150 000 km)─
        Console.WriteLine("\n=== 9. Stara vozila s visokom kilometražom ===");
        int pragGodina = DateTime.Now.Year - 7;
        var visokKm = svaVozila
            .Where(v => v.Godiste <= pragGodina && v.TrenutnaKilometraza > 150000)
            .OrderByDescending(v => v.TrenutnaKilometraza)
            .ToList();
        visokKm.ForEach(v => Console.WriteLine($"  {v.Naziv} – {v.TrenutnaKilometraza:N0} km"));

        // ── Upit 10: Podupiti – nalozi koji koriste barem jedan dio kategorije Motor─
        Console.WriteLine("\n=== 10. Nalozi koji uključuju motorni dio ===");
        var motorNalozi = sviNalozi
            .Where(n => n.Stavke.Any(s => s.Dio.Kategorija == KategorijaDijela.Motor))
            .Select(n => new
            {
                Nalog = $"#{n.Id}",
                Automobil = n.Automobil.Naziv,
                MotorDijelovi = string.Join(", ", n.Stavke
                    .Where(s => s.Dio.Kategorija == KategorijaDijela.Motor)
                    .Select(s => s.Dio.Naziv))
            })
            .ToList();
        motorNalozi.ForEach(x => Console.WriteLine($"  Nalog {x.Nalog} ({x.Automobil}): {x.MotorDijelovi}"));

        Console.WriteLine("\nPritisnite bilo koji tipku za izlaz...");
        Console.ReadKey();
    }
}
