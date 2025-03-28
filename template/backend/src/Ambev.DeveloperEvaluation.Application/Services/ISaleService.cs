using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
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
        Task<GetSaleResult?> GetSaleAsync(Guid id);
        Task<IEnumerable<Sale>> GetSalesAsync(int page, int size);
        Task<CancelSaleResult> CancelSaleAsync(Guid id);
        Task<int> GetTotalSalesCountAsync();
    }
}
