using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAppData.RestApi.Models.Error;
using SmartAppData.RestApi.Models.Search;
using SmartAppData.Services.SearchService;
using SmartAppData.Services.SearchService.Models;

namespace SmartAppData.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISearchService _searchService;
        public SearchController(IMapper mapper, ISearchService searchService)
        {
            _mapper = mapper;
            _searchService = searchService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SearchResult), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> SearchAsync([FromBody] SearchResource resource, CancellationToken cancellationToken)
        {
            var searchModel = _mapper.Map<SearchResource, SearchModel>(resource);

            var result = await _searchService.Search(searchModel);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}
