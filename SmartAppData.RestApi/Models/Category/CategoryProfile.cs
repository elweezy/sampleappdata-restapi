using AutoMapper;

namespace SmartAppData.RestApi.Models.Category
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<DAL.Entities.Category, CategoryResource>();
            CreateMap<SaveCategoryResource, DAL.Entities.Category>();
        }
    }
}
