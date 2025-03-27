using Ambev.DeveloperEvaluation.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Commands
{
    public class CreateSaleCommand
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public List<CreateSaleItemDto> Items { get; set; }
    }
}
