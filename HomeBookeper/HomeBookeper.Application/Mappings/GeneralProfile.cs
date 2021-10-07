using AutoMapper;
using HomeBookeper.Application.Features.Products.Commands.CreateProduct;
using HomeBookeper.Application.Features.Products.Queries.GetAllProducts;
using HomeBookeper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBookeper.Application.Mappings
{
	public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
			CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
			CreateMap<CreateProductCommand, Product>();
			CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
		}
	}
}
