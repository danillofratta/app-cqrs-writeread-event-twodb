using Base.Core.Domain.Common;
using Product.Command.Domain.Enum;

namespace ProductCommandDomainEntities;

public class Product : BaseEntity
{    
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public Status Status { get; set; } = Status.Atived;

    public DateTime? CommercializedAt { get; set; }
    public DateTime? CommercializedCancelledAt { get; set; }

    public StatusCommercialization StatusCommercialization { get; set; } = StatusCommercialization.Created;

    public void SetCancelled()
    {
        this.StatusCommercialization = StatusCommercialization.Cancelled;
        this.Status = Status.Cancelled;

        base.SetUpdate();
        base.SetDelete();
    }

    public void SetCommercilized()
    {
        this.StatusCommercialization = StatusCommercialization.BeingMarketed;        
        this.CommercializedAt = DateTime.UtcNow;

        base.SetUpdate();
    }

    public void SetCommercilizedCanceleld()
    {
        this.StatusCommercialization = StatusCommercialization.Cancelled;        
        this.CommercializedCancelledAt = DateTime.UtcNow;

        base.SetUpdate();
    }
}