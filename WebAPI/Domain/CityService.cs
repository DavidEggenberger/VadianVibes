using System.Collections.Generic;

namespace WebAPI.Domain
{
    public class CityService
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
        public Dictionary<string, int> KeywordCount { get; set; } = new Dictionary<string, int>();
        public ScrapedInformation ScrapedInformation { get; set; } = new ScrapedInformation();
        public override string ToString()
        {
            return leistungsbezeichnung + direktion_name + art_der_dienstleistung + dienststelle_name + direktion_kurzbezeichnung + thema + kurzbeschreibung + dienststelle + durchfuhrende_abteilung + weitere_informationen;
        }
    }
}
