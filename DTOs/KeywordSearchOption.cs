using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum KeywordSearchOption
    {
        MustMatchAllKeywords = 0,
        MustMatchOnlyOneKeyword = 1
    }
}
