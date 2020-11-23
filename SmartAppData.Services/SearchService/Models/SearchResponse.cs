using SmartAppData.RestApi.Models.Response;

namespace SmartAppData.Services.SearchService.Models
{
    public class SearchResponse : BaseResponse<SearchResult>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="searchResult">Search result.</param>
        /// <returns>Response.</returns>
        public SearchResponse(SearchResult searchResult) : base(searchResult)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SearchResponse(string message) : base(message)
        {

        }
    }
}
