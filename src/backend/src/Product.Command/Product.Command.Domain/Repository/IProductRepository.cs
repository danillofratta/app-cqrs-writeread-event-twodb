using Base.Core.Domain.Common;

namespace Product.Command.Repository;

public interface IProductCommandRepository :
        ICommandRepositoryBase<ProductCommandDomainEntities.Product, Guid>
{

}

public interface IProductQueryRepository :
        IQueryRepositoryBase<ProductCommandDomainEntities.Product, Guid>
{

}
