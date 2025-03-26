using Ambev.DeveloperEvaluation.Application.Commands;
using Ambev.DeveloperEvaluation.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers
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
        public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
        {
            var sale = await _saleService.CreateSaleAsync(command);
            return CreatedAtAction(nameof(Get), new { id = sale.Id }, sale);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sale = await _saleService.GetSaleAsync(id);
            return sale is null ? NotFound() : Ok(sale);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var sales = await _saleService.GetSalesAsync(page, size);
            return Ok(sales);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var sale = await _saleService.CancelSaleAsync(id);
            return sale is null ? NotFound() : Ok(sale);
        }
    }
}
