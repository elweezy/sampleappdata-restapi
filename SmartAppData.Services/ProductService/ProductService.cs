using SmartAppData.DAL.Entities;
using SmartAppData.Repositories.CategoryRepository;
using SmartAppData.Repositories.ProductRepository;
using SmartAppData.Repositories.UnitOfWork;
using SmartAppData.RestApi.Models.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
            )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<Product>> ListAsync(CancellationToken cancellationToken)
        {

            return await _productRepository.GetAllAsync(cancellationToken);
        }

        public async Task<ProductResponse> SaveAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId, cancellationToken);

                if (existingCategory == null)
                {
                    return new ProductResponse("Invalid category.");
                }

                await _productRepository.AddAsync(product, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id, cancellationToken);

            if (existingProduct == null)
            {
                return new ProductResponse("Product not found.");
            }

            var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId, cancellationToken);

            if (existingCategory == null)
            {
                return new ProductResponse("Invalid category.");
            }

            existingProduct.Name = product.Name;
            existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
            existingProduct.QuantityInPackage = product.QuantityInPackage;
            existingProduct.CategoryId = product.CategoryId;

            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when updating the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id, cancellationToken);

            if (existingProduct == null)
            {
                return new ProductResponse("Product not found.");
            }

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when deleting the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> SoftDeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id, cancellationToken);

            if (existingProduct == null)
            {
                return new ProductResponse("Product not found.");
            }

            try
            {
                existingProduct.Deleted = true;
                _productRepository.Update(existingProduct);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when deleting the product: {ex.Message}");
            }
        }
    }
}
