using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleResult
    {
        public Guid SaleId { get; set; }
        public bool Cancelled { get; set; }

        public CancelSaleResult(Guid saleId, bool cancelled)
        {
            SaleId = saleId;
            Cancelled = cancelled;
        }
    }
}
