using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class CancelSaleRequestProfile : Profile
    {
        public CancelSaleRequestProfile()
        {
            CreateMap<CancelSaleRequest, CancelSaleCommand>();
            CreateMap<CreateSaleRequest, CreateSaleCommand>();            
            CreateMap<CreateSaleRequestItem, CreateSaleItemDto>();
        }
    }
}
