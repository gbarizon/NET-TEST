﻿using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler
    {
        private readonly ISaleRepository _repository;

        public GetSaleHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetSaleResult?> HandleAsync(GetSaleQuery query)
        {
            var sale = await _repository.GetByIdAsync(query.SaleId);
            if (sale is null) return null;

            return new GetSaleResult
            {
                Id = sale.Id,
                Date = sale.Date,
                CustomerName = sale.CustomerName,
                BranchName = sale.BranchName,
                TotalAmount = sale.TotalAmount,
                Cancelled = sale.Cancelled,
                Items = sale.Items
            };
        }
    }
}
