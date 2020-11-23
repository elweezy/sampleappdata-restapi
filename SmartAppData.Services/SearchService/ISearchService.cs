using SmartAppData.Services.SearchService.Models;
using System.Threading.Tasks;

namespace SmartAppData.Services.SearchService
{
    public interface ISearchService
    {
        Task<SearchResponse> Search(SearchModel searchModel);
    }
}
