using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CityServiceGroupBy
    {
        art_der_dienstleistung,
        dienststelle_name,
        thema,
        dienststelle
    }
}
