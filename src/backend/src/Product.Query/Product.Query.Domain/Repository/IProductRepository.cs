using Base.Core.Domain.Common;

namespace Product.Query.Domain.Repository;

public interface IProductQueryRepository : IQueryRepositoryBase<ProductQueryDomainEntities.Product, Guid>
{
    Task<List<ProductQueryDomainEntities.Product>> GetByName(string name);
}


