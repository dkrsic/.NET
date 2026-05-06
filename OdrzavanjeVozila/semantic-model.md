# Semantic Database Model - OdrzavanjeVozila

## Pregled modela/klasa/tablica

Aplikacija **OdrzavanjeVozila** je sustav za upravljanje održavanjem vozila. Sastoji se od sljedećih entiteta (klasa/tablica):

### 1. **Korisnik** (Users)
- **Primarni ključ:** Id (int)
- **Svojstva:**
  - Ime (string)
  - Prezime (string)
  - Email (string)
  - Telefon (string)
  - Adresa (string)
  - DatumRegistracije (DateTime)
- **Veze:** 1-N s tablicom **Automobil**

### 2. **Automobil** (Cars)
- **Primarni ključ:** Id (int)
- **Strani ključ:** KorisnikId → Korisnik.Id
- **Svojstva:**
  - Marka (string)
  - Model (string)
  - Godiste (int)
  - RegistracijskiBroj (string)
  - BrojSasije (string)
  - TrenutnaKilometraza (int)
  - VrstaPogona (enum: Benzin, Diesel, Hybrid, Elektricni)
  - DatumPrvogServisa (DateTime)
- **Veze:**
  - N-1 s tablicom **Korisnik**
  - 1-N s tablicom **ServisniNalog**

### 3. **Radionica** (Workshops)
- **Primarni ključ:** Id (int)
- **Svojstva:**
  - Naziv (string)
  - Adresa (string)
  - Telefon (string)
  - Email (string)
- **Veze:** 1-N s tablicom **Mehanicar**

### 4. **Mehanicar** (Mechanics)
- **Primarni ključ:** Id (int)
- **Strani ključevi:**
  - RadionicaId → Radionica.Id
- **Svojstva:**
  - Ime (string)
  - Prezime (string)
  - Specijalizacija (string)
  - DatumZaposlenja (DateTime)
  - SatnicaEUR (decimal)
- **Veze:**
  - N-1 s tablicom **Radionica**
  - 1-N s tablicom **ServisniNalog**

### 5. **ServisniNalog** (Service Orders)
- **Primarni ključ:** Id (int)
- **Strani ključevi:**
  - AutomobilId → Automobil.Id
  - MehanicarId → Mehanicar.Id
- **Svojstva:**
  - DatumOtvaranja (DateTime)
  - DatumZatvaranja (DateTime?, nullable)
  - SljedecaPreporucenaPregleda (DateTime?, nullable)
  - OpisRadova (string)
  - UkupnaCijena (decimal)
  - Status (enum: Otvoren, URedu, Zatvoren)
  - KilometrazaPrilikomServisa (int)
  - Napomena (string)
- **Veze:**
  - N-1 s tablicom **Automobil**
  - N-1 s tablicom **Mehanicar**
  - 1-N s tablicom **NalogStavka**

### 6. **NalogStavka** (Service Order Items)
- **Primarni ključ:** Id (int)
- **Strani ključevi:**
  - NalogId → ServisniNalog.Id
  - DioId → Dio.Id
- **Svojstva:**
  - Kolicina (int)
  - CijenaKomad (decimal)
  - Napomena (string)
  - UkupnaCijenaStavke (computed: Kolicina * CijenaKomad)
- **Veze:**
  - N-1 s tablicom **ServisniNalog**
  - N-1 s tablicom **Dio**

### 7. **Dio** (Parts/Components)
- **Primarni ključ:** Id (int)
- **Svojstva:**
  - Naziv (string)
  - KatalogBroj (string)
  - Proizvodac (string)
  - Cijena (decimal)
  - Kategorija (enum: Filtri, Tekucine, RemeniIKaisle, Kocnice, Ostalo)
  - KolicinaNaSkladistu (int)
  - Opis (string)
- **Veze:** 1-N s tablicom **NalogStavka**

## Dijagram veza između tablica

```
Korisnik (1) ─── (N) Automobil
                         │
                         └─ (1) ─── (N) ServisniNalog ─── (1) ─── (N) NalogStavka ─── (N) Dio
                                          │
                                          └─ (N) ─── (1) Mehanicar ─── (N) Radionica
```

## Tipovi podataka i enumeracije

### VrstaPogona
- Benzin
- Diesel
- Hybrid
- Elektricni

### StatusNaloga
- Otvoren
- URedu
- Zatvoren

### KategorijaDijela
- Filtri
- Tekucine
- RemeniIKaisle
- Kocnice
- Ostalo

## Pravila brisanja (Delete Behaviors)

- **Automobil**: Kaskadno brisanje kod korisnika (ako korisnik bude obrisan, obrisu se i njegovi automobili)
- **ServisniNalog**: Kaskadno brisanje kod automobila, ograničeno brisanje kod mehaničara
- **NalogStavka**: Kaskadno brisanje kod naloga, ograničeno brisanje kod dijelova
- **Mehanicar**: Ograničeno brisanje kod radnice (ne može se obrisati radionica ako ima mehaničara)
