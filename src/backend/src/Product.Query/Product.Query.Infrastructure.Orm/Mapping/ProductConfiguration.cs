using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Query.Infrastructure.Orm.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<ProductQueryDomainEntities.Product>
{
    public void Configure(EntityTypeBuilder<ProductQueryDomainEntities.Product> builder)
    {
        builder.HasKey(u => u.Id);
    }
}

