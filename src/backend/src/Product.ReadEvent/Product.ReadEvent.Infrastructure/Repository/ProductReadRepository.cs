
using Base.Infrastructure.Command.Orm.Repository;
using Base.Infrastructure.Query.Orm.Repository;
using Product.Query.Infrastructure.Orm;
using Product.ReadEvent.Domain.Repository;

namespace Product.ReadEvent.Infrastructure.Repository;
public class ProductReadEventQueryRepository : QueryRepositoryBase<ProductQueryDomainEntities.Product, Guid>, IProductReadEventQueryRepository
{
    private readonly ProductQueryDbContext _ProductQueryDbContext;

    public ProductReadEventQueryRepository(ProductQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _ProductQueryDbContext = defaultDbContext;
    }
}

public class ProductReadEventCommandRepository : CommandRepositoryBase<ProductQueryDomainEntities.Product, Guid>, IProductReadEventCommandRepository
{
    private readonly ProductQueryDbContext _ProductQueryDbContext;

    public ProductReadEventCommandRepository(ProductQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _ProductQueryDbContext = defaultDbContext;
    }
}


