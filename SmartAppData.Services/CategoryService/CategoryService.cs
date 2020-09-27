using SmartAppData.DAL.Entities;
using SmartAppData.Repositories.CategoryRepository;
using SmartAppData.Repositories.UnitOfWork;
using SmartAppData.RestApi.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
            )
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<IEnumerable<Category>> ListAsync(CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllAsync(cancellationToken);
        }

        public async Task<CategoryResponse> SaveAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                await _categoryRepository.AddAsync(category, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id, cancellationToken);

            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found.");
            }

            existingCategory.Name = category.Name;

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id, cancellationToken);

            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found.");

            }

            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> SoftDeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id, cancellationToken);

            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found.");

            }

            try
            {
                existingCategory.Deleted = true;
                _categoryRepository.Update(existingCategory);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}
