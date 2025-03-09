using Microsoft.EntityFrameworkCore;
using Product.Query.Infrastructure.Orm.Mapping;
using System.Reflection;

namespace Product.Query.Infrastructure.Orm;

public class ProductQueryDbContext : DbContext
{
    public DbSet<ProductQueryDomainEntities.Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

#if DEBUG
            //run docker with VS
            //todo craete appsettings
            //var conn = "Host=localhost;Port=5432;Username=admin;Password=root;Database=apisalestock;";
            var conn = "Host=localhost;Port=5432;Username=admin;Password=root;Database=product_read;";
            optionsBuilder.UseNpgsql(conn);
#else
        //run docker 
        //todo craete appsettings
        var conn = "Host=postgres_db;Port=5432;Username=admin;Password=root;Database=apitest;";
        optionsBuilder.UseNpgsql(conn);
#endif

        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
