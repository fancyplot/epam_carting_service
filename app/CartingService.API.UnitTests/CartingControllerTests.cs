using AutoMapper;
using CartingService.API.Controllers.V1;
using CartingService.Domain.Commands.V1.CreateCart;
using CartingService.Domain.Models.V1;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CartingService.API.UnitTests
{
    public class CartingControllerTests
    {
        [Fact]
        public async void PostAsync_CreatesACarting_ReturnsOK()
        {
            // Arrange
            int id = 1;
            string name = "test";
            decimal price = 1.28m;
            int quantity = 3;
            string image = "";

            var cart = new Cart()
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Image = image
            };

            var mockMapper = new Mock<IMapper>();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(
                    t => t.Send(
                        It.Is<CreateCartCommand>(q => true),
                        It.Is<CancellationToken>(q => true)))
                .ReturnsAsync(cart);

            var controller = new CartingController(mockMediator.Object, mockMapper.Object);

            // Act
            var result  = await controller.PostAsync(id, name, price, quantity, image);

            // Assert
            using var scope = new AssertionScope();
            result.Should().BeOfType<CreatedResult>();
            var createdResult = result.As<CreatedResult>();
            createdResult.Location.Should().Be($"v1/carting/{id}");
        }


        [Fact]
        public async void PostAsync_TriesToCreateExistingCarting_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            string name = "test";
            decimal price = 1.28m;
            int quantity = 3;
            string image = "";
            string exception = $"Cart with id {id} already exists";

            var mockMapper = new Mock<IMapper>();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(
                    t => t.Send(
                        It.Is<CreateCartCommand>(q => true),
                        It.Is<CancellationToken>(q => true)))
                .Throws(new ArgumentException(exception));

            var controller = new CartingController(mockMediator.Object, mockMapper.Object);

            // Act
            var result = await controller.PostAsync(id, name, price, quantity, image);

            // Assert
            using var scope = new AssertionScope();
            result.Should().BeOfType<BadRequestObjectResult>();
            var createdResult = result.As<BadRequestObjectResult>();
            createdResult.Value.Should().Be(exception);
        }
    }
}