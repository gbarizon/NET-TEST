using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.MessageBrocker;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepoMock;
        private readonly Mock<IMessageBroker> _messageBrokerMock;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepoMock = new Mock<ISaleRepository>();
            _messageBrokerMock = new Mock<IMessageBroker>();

            _handler = new CreateSaleHandler(_saleRepoMock.Object, _messageBrokerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateSaleSuccessfully()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = "Cliente Teste",
                BranchId = Guid.NewGuid(),
                BranchName = "Filial Teste",
                Items = new()
                {
                    new CreateSaleItemDto
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Produto Teste",
                        Quantity = 5,
                        UnitPrice = 20m
                    }
                }
            };

            _saleRepoMock
                    .Setup(repo => repo.AddAsync(It.IsAny<Sale>()))
                    .Returns(Task.CompletedTask)
                    .Verifiable();

            _messageBrokerMock
                    .Setup(broker => broker.PublishAsync(It.IsAny<object>()))
                    .Returns(Task.CompletedTask)
                    .Verifiable();

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.CustomerId, result.CustomerId);
            Assert.Equal(command.BranchId, result.BranchId);
            Assert.Single(result.Items);
            Assert.False(result.Cancelled);
            Assert.Equal(90m, result.TotalAmount); // (5 * 20) - 10 = 90

            _saleRepoMock.Verify();
            _messageBrokerMock.Verify();
        }
    }
}
