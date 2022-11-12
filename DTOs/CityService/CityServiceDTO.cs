using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CityServiceDTO
    {
        public string leistungsbezeichnung { get; set; }
        public string direktion_name { get; set; }
        public string art_der_dienstleistung { get; set; }
        public string dienststelle_name { get; set; }
        public string direktion_kurzbezeichnung { get; set; }
        public string thema { get; set; }
        public string durchfuhrende_abteilung { get; set; }
        public string kurzbeschreibung { get; set; }
        public string direktlink_url { get; set; }
        public string merkblatt_link { get; set; }
        public string weitere_informationen { get; set; }
        public string dienststelle { get; set; }
    }
}
