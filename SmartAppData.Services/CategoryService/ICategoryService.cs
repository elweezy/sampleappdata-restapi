using SmartAppData.DAL.Entities;
using SmartAppData.RestApi.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> ListAsync(CancellationToken cancellationToken);
        Task<CategoryResponse> SaveAsync(Category category, CancellationToken cancellationToken);
        Task<CategoryResponse> UpdateAsync(int id, Category category, CancellationToken cancellationToken);
        Task<CategoryResponse> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<CategoryResponse> SoftDeleteAsync(int id, CancellationToken cancellationToken);
    }
}
