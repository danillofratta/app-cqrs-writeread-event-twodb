using Base.Core.Domain.Common;

namespace Product.ReadEvent.Domain.Repository;

public interface IProductReadEventCommandRepository : ICommandRepositoryBase<ProductQueryDomainEntities.Product, Guid>
{

}

public interface IProductReadEventQueryRepository : IQueryRepositoryBase<ProductQueryDomainEntities.Product, Guid>
{

}