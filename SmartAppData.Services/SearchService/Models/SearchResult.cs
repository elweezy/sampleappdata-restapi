using SmartAppData.RestApi.Models.ElasticSearch;
using System.Collections.Generic;

namespace SmartAppData.Services.SearchService.Models
{
    public class SearchResult
    {
        public List<Management> Managements { get; set; }
        public List<Property> Properties { get; set; }
    }
}
