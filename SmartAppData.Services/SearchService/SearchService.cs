using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;
using SmartAppData.RestApi.Models.ElasticSearch;
using SmartAppData.Services.SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly IElasticClient _elasticClient;

        public SearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<SearchResponse> Search(SearchModel searchModel)
        {
            var searchResult = new SearchResult();

            var managements = await _elasticClient.SearchAsync<ManagementObj>(
                s => s
                .Index("managements")
                .Query(
                    q => q
                    .MultiMatch(
                        mm => mm
                        .Fields(
                            f => f
                            .Field(f => f.mgmt.market)
                            .Field(f => f.mgmt.name)
                            .Field(f => f.mgmt.state)
                            )
                        .Query(searchModel.Phrase)
                        )
                    )
                .Size(searchModel.Limit)
                );

            var properties = await _elasticClient.SearchAsync<PropertyObj>(
                s => s
                .Index("properties")
                .Query(
                    q => q
                    .MultiMatch(
                        mm => mm
                        .Fields(
                            f => f
                            .Field(f => f.Property.market)
                            .Field(f => f.Property.name)
                            .Field(f => f.Property.streetAddress)
                            .Field(f => f.Property.city)
                            .Field(f => f.Property.formerName))
                        .Query(searchModel.Phrase)
                        )
                    )
                .Size(searchModel.Limit)
                );
            var managementResults = managements.Hits.Select(s => s.Source.mgmt).ToList();
            var propertyResults = properties.Hits.Select(s => s.Source.Property).ToList();

            if (searchModel.Markets.Any())
            {
                managementResults = managementResults.Where(x => searchModel.Markets.Contains(x.market)).ToList();
                propertyResults = propertyResults.Where(x => searchModel.Markets.Contains(x.market)).ToList();
            }

            searchResult.Managements = managementResults;
            searchResult.Properties = propertyResults;

            return await Task.FromResult(new SearchResponse(searchResult));
        }
    }
}
