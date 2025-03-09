using Base.Core.Domain.Specification;

namespace Product.Command.Domain.Specification
{
    public class ProductCanBeCancelledCommercializationSpecification : BaseSpecification<ProductCommandDomainEntities.Product>
    {
        public override bool IsSatisfiedBy(ProductCommandDomainEntities.Product entity)
        {
            return entity.StatusCommercialization == Product.Command.Domain.Enum.StatusCommercialization.Created ||
                entity.StatusCommercialization == Product.Command.Domain.Enum.StatusCommercialization.BeingMarketed;
        }
    }
}
