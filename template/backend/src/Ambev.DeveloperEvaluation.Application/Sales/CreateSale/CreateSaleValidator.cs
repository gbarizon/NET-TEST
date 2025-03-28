using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator
    {
        public static void Validate(IEnumerable<CreateSaleItemDto> items)
        {
            foreach (var item in items)
            {
                if (item.Quantity > 20)
                    throw new InvalidOperationException("Quantidade acima do permitido.");

                if (item.Quantity <= 0)
                    throw new InvalidOperationException("Quantidade inválida.");
            }
        }
    }
}
