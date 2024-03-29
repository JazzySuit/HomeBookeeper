﻿using HomeBookeper.Application.Interfaces.Repositories;
using HomeBookeper.Domain.Entities;
using HomeBookeper.Infrastructure.Persistence.Contexts;
using HomeBookeper.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeBookeper.Infrastructure.Persistence.Repositories;

public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
{
	private readonly DbSet<Product> _products;

	public ProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
	{
		_products = dbContext.Set<Product>();
	}

	public Task<bool> IsUniqueBarcodeAsync(string barcode)
	{
		return _products
			.AllAsync(p => p.Barcode != barcode);
	}
}
