using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAppData.Services.SearchService.Models
{
    public class SearchModel
    {
        public string Phrase { get; set; }
        public List<string> Markets { get; set; }
        public int Limit { get; set; }
    }
}
