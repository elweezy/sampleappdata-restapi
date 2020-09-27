using SmartAppData.DAL.Entities;
using SmartAppData.RestApi.Models.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Services.ProductService
{
    public interface IProductService
    {
        Task<IList<Product>> ListAsync(CancellationToken cancellationToken);
        Task<ProductResponse> SaveAsync(Product product, CancellationToken cancellationToken);
        Task<ProductResponse> UpdateAsync(int id, Product product, CancellationToken cancellationToken);
        Task<ProductResponse> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ProductResponse> SoftDeleteAsync(int id, CancellationToken cancellationToken);
    }
}
