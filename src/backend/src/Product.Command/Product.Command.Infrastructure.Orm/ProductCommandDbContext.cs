using Microsoft.EntityFrameworkCore;
using Shared.Infrasctructure.Orm.Mapping;
using System.Reflection;

namespace Shared.Infrastructure.Orm;

public class ProductCommandDbContext : DbContext
{
    public DbSet<ProductCommandDomainEntities.Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

#if DEBUG
            //run docker with VS
            //todo craete appsettings
            //var conn = "Host=localhost;Port=5432;Username=admin;Password=root;Database=apisalestock;";
            var conn = "Host=localhost;Port=5432;Username=admin;Password=root;Database=product_write;";
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
