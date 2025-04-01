using Ambev.DeveloperEvaluation.MessageBrocker;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler
    {
        private readonly ISaleRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public CreateSaleHandler(ISaleRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task<Sale> HandleAsync(CreateSaleCommand command)
        {
            CreateSaleValidator.Validate(command.Items);

            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                CustomerId = command.CustomerId,
                CustomerName = command.CustomerName,
                BranchId = command.BranchId,
                BranchName = command.BranchName,
                Cancelled = false
            };

            foreach (var itemDto in command.Items)
            {
                decimal discount = CalculateDiscount(itemDto.Quantity, itemDto.UnitPrice);

                sale.Items.Add(new SaleItem
                {
                    ProductId = itemDto.ProductId,
                    ProductName = itemDto.ProductName,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    Discount = discount
                });
            }

            await _repository.AddAsync(sale);
            await _messageBroker.PublishAsync(new SaleCreatedEvent(sale));

            return sale;
        }

        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity >= 10)
                return unitPrice * quantity * 0.20m;
            if (quantity >= 4)
                return unitPrice * quantity * 0.10m;

            return 0m;
        }        
    }
}
