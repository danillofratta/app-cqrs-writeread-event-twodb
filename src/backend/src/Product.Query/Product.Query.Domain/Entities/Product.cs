using Base.Core.Domain.Common;

namespace ProductQueryDomainEntities;

public class Product : BaseEntity
{    
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Status { get; set; } 

    public DateTime? CommercializedAt { get; set; }
    public DateTime? CommercializedCancelledAt { get; set; }

    public string StatusCommercialization { get; set; } 
}