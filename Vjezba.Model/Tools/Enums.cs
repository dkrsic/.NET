using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model.Tools
{
    public enum StatusNaloga
    {
        Otvoren,
        UObradi,
        Zavrsen,
        Otkazan
    }

    public enum KategorijaDijela
    {
        Motor,
        Kocnice,
        Ovjes,
        Elektrika,
        Karoserija,
        Gume,
        Ostalo
    }

    public enum VrstaPogona
    {
        Benzin,
        Dizel,
        Elektricni,
        Hibridni,
        Plin
    }
}
