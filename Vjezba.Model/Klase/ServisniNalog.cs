using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vjezba.Model.Tools;

namespace Vjezba.Model.Klase
{
    public class ServisniNalog
    {
        public int Id { get; set; }
        public DateTime DatumOtvaranja { get; set; }
        public DateTime? DatumZatvaranja { get; set; }
        public DateTime? SljedecaPreporucenaPregleda { get; set; }
        public string OpisRadova { get; set; }
        public decimal UkupnaCijena { get; set; }
        public StatusNaloga Status { get; set; }
        public int KilometrazaPrilikomServisa { get; set; }
        public string Napomena { get; set; }

        public int AutomobilId { get; set; }
        public Automobil Automobil { get; set; }
        public int MehanicarId { get; set; }
        public Mehanicar Mehanicar { get; set; }

        public List<NalogStavka> Stavke { get; set; }

        public ServisniNalog()
        {
            Stavke = new List<NalogStavka>();
            DatumOtvaranja = DateTime.Now;
            Status = StatusNaloga.Otvoren;
        }
    }
}
