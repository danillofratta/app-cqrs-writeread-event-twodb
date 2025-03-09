
using Base.Infrastructure.Command.Orm.Repository;
using Base.Infrastructure.Query.Orm.Repository;
using Product.Command.Repository;
using Shared.Infrastructure.Orm;

namespace Product.Infrastructure.Orm.Repository;

public class ProductCommandRepository : CommandRepositoryBase<ProductCommandDomainEntities.Product, Guid>, IProductCommandRepository
{
    private readonly ProductCommandDbContext _ProductCommandDbContext;

    public ProductCommandRepository(ProductCommandDbContext defaultDbContext) : base(defaultDbContext)
    {
        _ProductCommandDbContext = defaultDbContext;
    }   
}

public class ProductQueryRepository : QueryRepositoryBase<ProductCommandDomainEntities.Product, Guid>, IProductQueryRepository
{
    private readonly ProductCommandDbContext _ProductCommandDbContext;

    public ProductQueryRepository(ProductCommandDbContext defaultDbContext) : base(defaultDbContext)
    {
        _ProductCommandDbContext = defaultDbContext;    
    }
}
