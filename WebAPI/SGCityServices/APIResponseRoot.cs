using System.Collections.Generic;
using System;
using WebAPI.Domain;

namespace WebAPI.SGCityServicesClient
{
    public class Facet
    {
        public string name { get; set; }
        public int count { get; set; }
        public string state { get; set; }
        public string path { get; set; }
    }

    public class FacetGroup
    {
        public string name { get; set; }
        public List<Facet> facets { get; set; }
    }

    public class Parameters
    {
        public string dataset { get; set; }
        public int rows { get; set; }
        public int start { get; set; }
        public List<string> sort { get; set; }
        public List<string> facet { get; set; }
        public string format { get; set; }
        public string timezone { get; set; }
    }

    public class Record
    {
        public string datasetid { get; set; }
        public string recordid { get; set; }
        public CityService fields { get; set; }
        public DateTime record_timestamp { get; set; }
    }

    public class APIResponseRoot
    {
        public int nhits { get; set; }
        public Parameters parameters { get; set; }
        public List<Record> records { get; set; }
        public List<FacetGroup> facet_groups { get; set; }
    }
}
