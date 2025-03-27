using Ambev.DeveloperEvaluation.Application.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public interface ISaleService
    {
        Task<Sale> CreateSaleAsync(CreateSaleCommand command);
        Task<Sale> GetSaleAsync(Guid id);
        Task<IEnumerable<Sale>> GetSalesAsync(int page, int size);
        Task<Sale> CancelSaleAsync(Guid id);
    }
}
