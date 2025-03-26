using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; } // External Identity
        public string CustomerName { get; set; } // Denormalization
        public Guid BranchId { get; set; } // External Identity
        public string BranchName { get; set; } // Denormalization
        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
        public bool Cancelled { get; set; }

        public decimal TotalAmount => Items.Sum(i => i.TotalAmount);
    }
}
