using AutoMapper;
using Moq;
using SmartAppData.DAL.Entities;
using SmartAppData.Repositories.CategoryRepository;
using SmartAppData.Repositories.UnitOfWork;
using SmartAppData.RestApi.Controllers;
using SmartAppData.RestApi.Models.Category;
using SmartAppData.Services.CategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartAppData.RestApi.Tests
{
    public class CategoriesControllerTests
    {
        [Fact]
        public async Task ListAsync_ReturnsEmptyArrayWhenNoDataFound()
        {
            // Arrange
            var mockCategoriesService = new Mock<ICategoryService>();
            var mockMapper = new Mock<IMapper>();
            var controller = new CategoriesController(mockCategoriesService.Object, mockMapper.Object);

            // Act
            var result = await controller.ListAsync(new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task PostAsync_ReturnsDataWhenCreated()
        {
            // Arrange
            //var mockCategoryRepository = new Mock<ICategoryRepository>();
            //mockCategoryRepository.Setup(x => x.AddAsync(new DAL.Entities.Category() { Id = 1, Name = "testCategory" }, new System.Threading.CancellationToken()))
            //                      .Returns(Task.FromResult(1));

            //var mockUnitOfWork = new Mock<IUnitOfWork>();
            //mockUnitOfWork.Setup(x => x.SaveChangesAsync(new System.Threading.CancellationToken()))
            //              .Returns(Task.FromResult(1));

            var mockCategoryService = new Mock<ICategoryService>();
            //var categoryService = new CategoryService(mockCategoryRepository.Object, mockUnitOfWork.Object);

            var mockMapper = new Mock<IMapper>();
            var controller = new CategoriesController(mockCategoryService.Object, mockMapper.Object);
            var testResource = new SaveCategoryResource();
            testResource.Name = "testCategory";


            // Act
            var insertResult = await controller.PostAsync(testResource, new System.Threading.CancellationToken());
            var result = await controller.ListAsync(new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(insertResult);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("testCategory", result.ToList()[0].Name);
        }
    }
}
