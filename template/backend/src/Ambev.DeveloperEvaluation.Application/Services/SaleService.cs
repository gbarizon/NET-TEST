using Ambev.DeveloperEvaluation.MessageBrocker;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
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
            var handler = new CreateSaleHandler(_repository, _messageBroker);
            return await handler.HandleAsync(command);
        }

        public async Task<GetSaleResult?> GetSaleAsync(Guid id)
        {
            var handler = new GetSaleHandler(_repository);
            return await handler.HandleAsync(new GetSaleQuery(id));
        }

        public async Task<IEnumerable<Sale>> GetSalesAsync(int page, int size)
        {
            var handler = new GetSalesHandler(_repository);
            var result = await handler.HandleAsync(new GetSalesQuery(page, size));
            return result.Sales;
        }

        public async Task<CancelSaleResult> CancelSaleAsync(Guid id)
        {
            var handler = new CancelSaleHandler(_repository, _messageBroker);
            return await handler.HandleAsync(new CancelSaleCommand(id));
        }
        public async Task<int> GetTotalSalesCountAsync()
        {
            return await _repository.GetTotalCountAsync();
        }
    }
}
