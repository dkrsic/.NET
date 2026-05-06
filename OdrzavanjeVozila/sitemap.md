# Sitemap - Routing Model Aplikacije OdrzavanjeVozila

Ovaj dokument dokumentira sve dostupne URL-ove u aplikaciji, te naznačuje koji je controller, koja akcija i koji view-ovi se koriste.

## Standardne rute (default routing)

### Home Controller
| URL | Controller | Akcija | View |
|-----|-----------|--------|------|
| `/` | Home | Index | Views/Home/Index.cshtml |
| `/home` | Home | Index | Views/Home/Index.cshtml |
| `/home/Index` | Home | Index | Views/Home/Index.cshtml |
| `/home/privacy` | Home | Privacy | Views/Home/Privacy.cshtml |

### Korisnik Controller (Default routing)
| URL | Controller | Akcija | View |
|-----|-----------|--------|------|
| `/Korisnik` | Korisnik | Index | Views/Korisnik/Index.cshtml |
| `/Korisnik/Index` | Korisnik | Index | Views/Korisnik/Index.cshtml |
| `/Korisnik/Details/1` | Korisnik | Details | Views/Korisnik/Details.cshtml |

### Automobil Controller (Default routing)
| URL | Controller | Akcija | View |
|-----|-----------|--------|------|
| `/Automobil` | Automobil | Index | Views/Automobil/Index.cshtml |
| `/Automobil/Index` | Automobil | Index | Views/Automobil/Index.cshtml |
| `/Automobil/Details/1` | Automobil | Details | Views/Automobil/Details.cshtml |

### Mehanicar Controller (Default routing)
| URL | Controller | Akcija | View |
|-----|-----------|--------|------|
| `/Mehanicar` | Mehanicar | Index | Views/Mehanicar/Index.cshtml |
| `/Mehanicar/Index` | Mehanicar | Index | Views/Mehanicar/Index.cshtml |
| `/Mehanicar/Details/1` | Mehanicar | Details | Views/Mehanicar/Details.cshtml |

### Radionica Controller (Default routing)
| URL | Controller | Akcija | View |
|-----|-----------|--------|------|
| `/Radionica` | Radionica | Index | Views/Radionica/Index.cshtml |
| `/Radionica/Index` | Radionica | Index | Views/Radionica/Index.cshtml |
| `/Radionica/Details/1` | Radionica | Details | Views/Radionica/Details.cshtml |

### Dio Controller (Default routing)
| URL | Controller | Akcija | View |
|-----|-----------|--------|------|
| `/Dio` | Dio | Index | Views/Dio/Index.cshtml |
| `/Dio/Index` | Dio | Index | Views/Dio/Index.cshtml |
| `/Dio/Details/1` | Dio | Details | Views/Dio/Details.cshtml |

### ServisniNalog Controller (Default routing)
| URL | Controller | Akcija | View |
|-----|-----------|--------|------|
| `/ServisniNalog` | ServisniNalog | Index | Views/ServisniNalog/Index.cshtml |
| `/ServisniNalog/Index` | ServisniNalog | Index | Views/ServisniNalog/Index.cshtml |
| `/ServisniNalog/Details/1` | ServisniNalog | Details | Views/ServisniNalog/Details.cshtml |

---

## CUSTOM RUTE (Implementirane u Program.cs)

### Custom Route 1: Vozila po korisniku
```
Pattern: /vozila/korisnika/{korisnikId:int}
Controller: Automobil
Akcija: PoBenzinskoj
Opis: Prikazuje automobila specifičnog korisnika
Primjer: /vozila/korisnika/1
```

### Custom Route 2: Servisi po statusu
```
Pattern: /servisi/status/{status}
Controller: ServisniNalog
Akcija: PriStatusu
Opis: Prikazuje servisne naloge filtriranog po statusu
Primjer: /servisi/status/Otvoren
```

### Custom Route 3: Dijelovi po kategoriji
```
Pattern: /dijelovi/kategorija/{kategorija}
Controller: Dio
Akcija: PrioCategory
Opis: Prikazuje dijelove filtriranog po kategoriji
Primjer: /dijelovi/kategorija/Filtri
```

### Custom Route 4: Mehaničari po radionici
```
Pattern: /mehanicari/radionica/{radionicaId:int}
Controller: Mehanicar
Akcija: PorRadionica
Opis: Prikazuje mehaničare specifične radoce
Primjer: /mehanicari/radionica/1
```

---

## Attribute Routing (Ako budu implementirane na controllerima)

Zahtjevi za Lab 3 također omogućuju korištenje Attribute Routing direktno na controller akcijama, gdje se koristi `[Route]` atribut umjesto `MapControllerRoute` u `Program.cs`.

Primjer Attribute Routinga:
```csharp
[Route("auto")]
public class AutomobilController : Controller
{
    [Route("lista")]
    public IActionResult Index() { ... }
    
    [Route("{id:int}")]
    public IActionResult Details(int id) { ... }
}
```

---

## Redoslijed evaluacije ruta

1. **Attribute routes** - provjeravaju se prvi
2. **Custom routes** - provjeravaju se redoslijedo kako su definirane u `Program.cs`
3. **Default route** - fallback ako nijedna druga ruta nije odgovarajuća

---

## Bilješke

- Svi ID-evi u rutama trebaju biti cijeli brojevi (`:int`)
- Ako ID nije uključen u URL-u, defaultna vrijednost je null (opcionalno)
- Status i kategorija parametri u custom rutama trebaju biti stringovi koji odgovaraju enumeracijama
- Za potpuni CRUD sustav trebaju se implementirati akcije: Create, Edit, Delete (nisu još sve dokumentirane)
