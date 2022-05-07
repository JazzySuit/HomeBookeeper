﻿using AutoMapper;
using HomeBookeper.Application.Filters;
using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Application.Wrappers;
using MediatR;

namespace HomeBookeper.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<PagedResponse<IEnumerable<GetAllProductsViewModel>>>
{
	public int PageNumber { get; init; }
	public int PageSize { get; init; }
}

public class GetAllProductsQueryHandler 
	: IRequestHandler<GetAllProductsQuery, 
		PagedResponse<IEnumerable<GetAllProductsViewModel>>>
{
	private readonly IProductRepositoryAsync _productRepository;
	private readonly IMapper _mapper;

	public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper)
	{
		_productRepository = productRepository;
		_mapper = mapper;
	}

	public async Task<PagedResponse<IEnumerable<GetAllProductsViewModel>>> Handle(
		GetAllProductsQuery request, 
		CancellationToken cancellationToken)
	{
		var validFilter = _mapper.Map<GetAllProductsParameter>(request);
		var product = await _productRepository.GetPagedReponseAsync(
			validFilter.PageNumber, 
			validFilter.PageSize);

		var productViewModel = _mapper.Map<IEnumerable<GetAllProductsViewModel>>(product);

		return new PagedResponse<IEnumerable<GetAllProductsViewModel>>(
			productViewModel, 
			validFilter.PageNumber, 
			validFilter.PageSize);
	}
}
