using HomeBookeper.Domain.Common;

namespace HomeBookeper.Domain.Entities;

public class Product : AuditableBaseEntity
{
	public string Name { get; set; }
	public string Barcode { get; set; }
	public string Description { get; set; }
	public decimal Rate { get; set; }
}
