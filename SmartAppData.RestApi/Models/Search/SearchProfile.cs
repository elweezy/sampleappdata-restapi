using AutoMapper;
using SmartAppData.Services.SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAppData.RestApi.Models.Search
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<SearchResource, SearchModel>()
                .ForMember(dest => dest.Limit, opt => opt.MapFrom(src => src.Limit == 0 ? 25 : src.Limit));
        }
    }
}
