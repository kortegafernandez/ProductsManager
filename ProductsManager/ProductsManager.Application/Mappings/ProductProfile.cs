using AutoMapper;
using ProductsManager.Application.Products.Commands.Create;
using ProductsManager.Application.Products.Commands.Update;
using ProductsManager.Domain.DTOs;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();

        }
    }
}
