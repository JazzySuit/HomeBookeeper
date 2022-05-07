using AutoMapper;
using HomeBookeper.Application.Features.Books.Commands.CreateBook;
using HomeBookeper.Application.Features.Books.Queries.GetAllBooks;
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

		CreateMap<BoardBook, GetAllBooksViewModel>();
		CreateMap<CreateBookCommand, BoardBook>();

		CreateMap<ChildrensBook, GetAllBooksViewModel>();
		CreateMap<CreateBookCommand, ChildrensBook>();

		CreateMap<FictionBook, GetAllBooksViewModel>();
		CreateMap<CreateBookCommand, FictionBook>();

		CreateMap<NonFictionBook, GetAllBooksViewModel>();
		CreateMap<CreateBookCommand, NonFictionBook>();

		CreateMap<GetAllBooksQuery, GetAllBooksParameter>();
	}
}
