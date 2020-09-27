using Microsoft.EntityFrameworkCore;
using Moq;
using SmartAppData.DAL.Entities;
using SmartAppData.Persistence.Contexts;
using SmartAppData.Repositories.CategoryRepository;
using SmartAppData.Repositories.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartAppData.Repositories.Tests
{
    public class CategoryRepositoryTests
    {

        [Fact]
        public async Task AddAsync_ReturnsCategory()
        {
            // Arrange
            var dbContext = GetSmartAppDataDbContext();
            ICategoryRepository sut = GetInMemoryCategoryRepository(dbContext);
            IUnitOfWork unitOfWork = GetUnitOfWork(dbContext);
            Category category = new Category() { Name = "testCategory" };

            // Act
            await sut.AddAsync(category, new System.Threading.CancellationToken());
            await unitOfWork.SaveChangesAsync(new System.Threading.CancellationToken());
            var result = await sut.GetAllAsync(new System.Threading.CancellationToken());

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(result.ToList()[2].Name, category.Name);
        }

        private SmartAppDataDbContext GetSmartAppDataDbContext()
        {
            DbContextOptions<SmartAppDataDbContext> options;
            var builder = new DbContextOptionsBuilder<SmartAppDataDbContext>();
            builder.UseInMemoryDatabase("test-db");
            options = builder.Options;
            SmartAppDataDbContext smartAppDataDbContext = new SmartAppDataDbContext(options);
            smartAppDataDbContext.Database.EnsureDeleted();
            smartAppDataDbContext.Database.EnsureCreated();
            return smartAppDataDbContext;
        }

        private ICategoryRepository GetInMemoryCategoryRepository(SmartAppDataDbContext smartAppDataDbContext)
        {
            return new CategoryRepository.CategoryRepository(smartAppDataDbContext);
        }

        private IUnitOfWork GetUnitOfWork(SmartAppDataDbContext smartAppDataDbContext)
        {

            return new UnitOfWork.UnitOfWork(smartAppDataDbContext);
        }
    }
}
