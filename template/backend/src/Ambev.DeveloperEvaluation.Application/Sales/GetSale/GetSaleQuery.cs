using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleQuery
    {
        public Guid SaleId { get; set; }

        public GetSaleQuery(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
