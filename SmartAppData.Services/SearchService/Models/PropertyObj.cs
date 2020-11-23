using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAppData.RestApi.Models.ElasticSearch
{
    [ElasticsearchType(RelationName = "property")]
    public class PropertyObj
    {
        public Property Property { get; set; }
    }

    public class Property
    {
        public long propertyID { get; set; }
        [Text(Analyzer = "mynGram")]
        public string name { get; set; }
        [Text(Analyzer = "mynGram")]
        public string formerName { get; set; }
        [Text(Analyzer = "mynGram")]
        public string streetAddress { get; set; }
        [Text(Analyzer = "mynGram")]
        public string city { get; set; }
        [Text(Analyzer = "mynGram")]
        public string market { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }

    }
}
