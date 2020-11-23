using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAppData.Services.SearchService.Models
{
    [ElasticsearchType(RelationName="management")]
    public class ManagementObj
    {
        public Management mgmt { get; set; }
    }

    public class Management
    {
        public long mgmtID { get; set; }
        [Text(Analyzer = "mynGram")]
        public string name { get; set; }
        [Text(Analyzer = "mynGram")]
        public string market { get; set; }
        [Text(Analyzer = "mynGram")]
        public string state { get; set; }
    }
}
