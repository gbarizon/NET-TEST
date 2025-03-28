using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesQuery
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public GetSalesQuery(int page, int size)
        {
            Page = page;
            Size = size;
        }
    }
}
