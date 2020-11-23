using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartAppData.RestApi.Models.Search
{
    public class SearchResource
    {
        public SearchResource()
        {
            Limit = 25;
        }

        [Required]
        public string Phrase { get; set; }
        public List<string> Markets { get; set; }
        public int Limit { get; set; }
    }
}
