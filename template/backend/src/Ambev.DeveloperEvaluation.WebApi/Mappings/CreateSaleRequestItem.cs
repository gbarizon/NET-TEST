﻿namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class CreateSaleRequestItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
