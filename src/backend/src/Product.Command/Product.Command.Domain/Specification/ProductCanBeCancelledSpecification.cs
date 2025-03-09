using Base.Core.Domain.Specification;

namespace Product.Command.Domain.Specification
{
    public class ProductCanBeCancelledSpecification : BaseSpecification<ProductCommandDomainEntities.Product>
    {
        public override bool IsSatisfiedBy(ProductCommandDomainEntities.Product entity)
        {
            return entity.Status == Product.Command.Domain.Enum.Status.Created ||
                entity.Status == Product.Command.Domain.Enum.Status.Atived;
        }
    }
}
