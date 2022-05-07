namespace HomeBookeper.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsViewModel
{
	public int Id { get; init; }
	public string Name { get; init; }
	public string Barcode { get; init; }
	public string Description { get; init; }
	public decimal Rate { get; init; }
}
