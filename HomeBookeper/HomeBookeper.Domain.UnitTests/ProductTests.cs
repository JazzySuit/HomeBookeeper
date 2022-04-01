using HomeBookeper.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace HomeBookeper.Domain.UnitTests
{
	public class ProductTests
	{
		[Fact]
		public void CanCreateProduct()
		{
			const string name = "Bob";
			const string barcode = "122345667";
			const string userEmail = "basicuser@gmail.com";
			const decimal productRate = 50.5M;
			DateTime createdDate = new DateTime(2020, 5, 12);

			var product = new Product()
			{
				Name = name,
				Barcode = barcode,
				CreatedBy = userEmail,
				Created = createdDate,
				Rate = productRate
			};

			product.Name.Should().Be(name);
			product.Barcode.Should().Be(barcode);
			product.CreatedBy.Should().Be(userEmail);
			product.Created.Should().Be(createdDate);
			product.Rate.Should().Be(productRate);
		}
	}
}