using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesResult
    {
        public IEnumerable<Sale> Sales { get; set; }

        public GetSalesResult(IEnumerable<Sale> sales)
        {
            Sales = sales;
        }
    }
}
