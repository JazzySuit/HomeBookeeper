using AutoMapper;
using HomeBookeper.Application.Features.Books.Commands.CreateBook;
using HomeBookeper.Application.Features.Products.Commands.CreateProduct;
using HomeBookeper.Application.Features.Products.Queries.GetAllProducts;
using HomeBookeper.Domain.Entities;

namespace HomeBookeper.Application.Mappings;

public class GeneralProfile : Profile
{
	public GeneralProfile()
	{
		CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
		CreateMap<CreateProductCommand, Product>();
		CreateMap<GetAllProductsQuery, GetAllProductsParameter>();

		CreateMap<CreateBookCommand, Book>();
	}
}
