using Amazon;
using Amazon.Runtime.CredentialManagement;
using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAppData.RestApi.Extensions
{
    public static class ElasticSearchExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetAWSOptions();
            var httpConnection = new AwsHttpConnection(options);
            var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("AWS:ElasticUrl").Value));
            var config = new ConnectionSettings(pool, httpConnection);
            var client = new ElasticClient(config);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
