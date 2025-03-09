using Base.Infrastructure.Query.Orm.Repository;
using Microsoft.EntityFrameworkCore;
using Product.Query.Domain.Repository;

namespace Product.Query.Infrastructure.Orm.Repository;

public class ProductQueryRepository : QueryRepositoryBase<ProductQueryDomainEntities.Product, Guid>, IProductQueryRepository
{
    private readonly ProductQueryDbContext _ProductQueryDbContext;

    public ProductQueryRepository(ProductQueryDbContext defaultDbContext) : base(defaultDbContext)
    {
        _ProductQueryDbContext = defaultDbContext;
    }

    public async Task<List<ProductQueryDomainEntities.Product>> GetByName(string name)
    {
        var products = await _ProductQueryDbContext.Products.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToListAsync();

        return products;
    }    
}



