using Microsoft.EntityFrameworkCore;
using SmartAppData.DAL.Entities;
using SmartAppData.Persistence.Contexts;
using SmartAppData.Repositories.CategoryRepository;
using SmartAppData.Repositories.ProductRepository;
using SmartAppData.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartAppData.Repositories.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ReturnsProduct()
        {
            // Arrange
            var dbContext = GetSmartAppDataDbContext();
            IProductRepository sut = GetInMemoryProductRepository(dbContext);
            IUnitOfWork unitOfWork = GetUnitOfWork(dbContext);
            var category = new Category() { Name = "testCategory" };

            Product product = new Product() { Name = "testProduct", CategoryId = 100 };

            // Act
            await sut.AddAsync(product, new System.Threading.CancellationToken());
            await unitOfWork.SaveChangesAsync(new System.Threading.CancellationToken());

            var result = await sut.GetAllAsync(new System.Threading.CancellationToken());

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(result.ToList()[2].Name, product.Name);
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

        private IProductRepository GetInMemoryProductRepository(SmartAppDataDbContext smartAppDataDbContext)
        {
            return new ProductRepository.ProductRepository(smartAppDataDbContext);
        }

        private IUnitOfWork GetUnitOfWork(SmartAppDataDbContext smartAppDataDbContext)
        {

            return new UnitOfWork.UnitOfWork(smartAppDataDbContext);
        }
    }
}
