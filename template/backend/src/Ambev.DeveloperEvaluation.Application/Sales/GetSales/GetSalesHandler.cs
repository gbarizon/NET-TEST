using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesHandler
    {
        private readonly ISaleRepository _repository;

        public GetSalesHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetSalesResult> HandleAsync(GetSalesQuery query)
        {
            var sales = await _repository.GetAllAsync(query.Page, query.Size);
            return new GetSalesResult(sales);
        }
    }
}
