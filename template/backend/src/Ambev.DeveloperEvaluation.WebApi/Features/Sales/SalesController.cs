using Ambev.DeveloperEvaluation.Adapters.Driven.MessageBrokers.MessageBrocker;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
        [FromBody] CreateSaleRequest request,
        [FromServices] IMapper mapper)
        {
            var command = mapper.Map<CreateSaleCommand>(request);
            var sale = await _saleService.CreateSaleAsync(command);
            return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _saleService.GetSaleAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var sales = await _saleService.GetSalesAsync(page, size);
            var totalCount = await _saleService.GetTotalSalesCountAsync();

            var paginatedList = new PaginatedList<Sale>(sales.ToList(), totalCount, page, size);

            var response = new PaginatedResponse<Sale>
            {
                Data = paginatedList,          
                CurrentPage = paginatedList.CurrentPage,
                TotalPages = paginatedList.TotalPages,
                TotalCount = paginatedList.TotalCount,                
                Message = "Sales retrieved successfully."
            };

            return Ok(response);
        }

        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(
        [FromRoute] Guid id,
        [FromServices] IMapper mapper)
        {
            var request = new CancelSaleRequest { SaleId = id };
            var command = mapper.Map<CancelSaleCommand>(request);

            var result = await _saleService.CancelSaleAsync(command.SaleId);
            return Ok(result);
        }
    }
}
