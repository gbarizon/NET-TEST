using Ambev.DeveloperEvaluation.Adapters.Driven.MessageBrokers.MessageBrocker;
using Ambev.DeveloperEvaluation.Application.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public SaleService(ISaleRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task<Sale> CreateSaleAsync(CreateSaleCommand command)
        {
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
                if (itemDto.Quantity > 20)
                    throw new InvalidOperationException("It is not allowed to sell more than 20 items of the same product.");

                decimal discount = 0;
                if (itemDto.Quantity >= 10)
                    discount = itemDto.UnitPrice * itemDto.Quantity * 0.20m;
                else if (itemDto.Quantity >= 4)
                    discount = itemDto.UnitPrice * itemDto.Quantity * 0.10m;

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

        public async Task<Sale> GetSaleAsync(Guid id)
        {
            var sale = await _repository.GetByIdAsync(id);
            if (sale == null)
                throw new InvalidOperationException("Sale not found!");

            return sale;
        }

        public async Task<IEnumerable<Sale>> GetSalesAsync(int page, int size)
        {
            return await _repository.GetAllAsync(page, size);
        }

        public async Task<Sale> CancelSaleAsync(Guid id)
        {
            var sale = await _repository.GetByIdAsync(id);

            if (sale == null)
                throw new InvalidOperationException("Sale not found!");

            sale.Cancelled = true;

            await _repository.UpdateAsync(sale);
            await _messageBroker.PublishAsync(new SaleCancelledEvent(sale.Id));

            return sale;
        }
    }
}
