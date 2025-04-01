using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.MessageBrocker
{
    public class SaleMessageBroker : IMessageBroker
    {
        private readonly ILogger<SaleMessageBroker> _logger;

        public SaleMessageBroker(ILogger<SaleMessageBroker> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync<TEvent>(TEvent eventMessage) where TEvent : class
        {
            switch (eventMessage)
            {
                case SaleCreatedEvent createdEvent:
                    _logger.LogInformation("Evento publicado: Venda criada - Id: {SaleId}, Data: {Date}",
                        createdEvent.Sale.Id, createdEvent.Sale.Date);
                    break;

                case SaleCancelledEvent cancelledEvent:
                    _logger.LogInformation("Evento publicado: Venda cancelada - Id: {SaleId}",
                        cancelledEvent.SaleId);
                    break;

                case SaleModifiedEvent modifiedEvent:
                    _logger.LogInformation("Evento publicado: Venda modificada - Id: {SaleId}",
                        modifiedEvent.Sale.Id);
                    break;

                case ItemCancelledEvent itemCancelledEvent:
                    _logger.LogInformation("Evento publicado: Item cancelado - Produto Id: {ProductId} da Venda: {SaleId}",
                        itemCancelledEvent.ProductId, itemCancelledEvent.SaleId);
                    break;

                default:
                    _logger.LogInformation("Evento desconhecido publicado: {EventType}",
                        typeof(TEvent).Name);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
