using HomeBookeper.Domain.Entities;

namespace HomeBookeper.Application.Interfaces.Repositories
{
	public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
	{
		Task<bool> IsUniqueBarcodeAsync(string barcode);
	}
}
