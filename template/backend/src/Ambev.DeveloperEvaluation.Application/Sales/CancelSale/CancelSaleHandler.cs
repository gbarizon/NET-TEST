using Ambev.DeveloperEvaluation.MessageBroker;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler
    {
        private readonly ISaleRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public CancelSaleHandler(ISaleRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task<CancelSaleResult> HandleAsync(CancelSaleCommand command)
        {
            var sale = await _repository.GetByIdAsync(command.SaleId)
                ?? throw new InvalidOperationException("Venda não encontrada.");

            sale.Cancelled = true;

            await _repository.UpdateAsync(sale);
            await _messageBroker.PublishAsync(new SaleCancelledEvent(sale.Id));

            return new CancelSaleResult(sale.Id, sale.Cancelled);
        }
    }
}
