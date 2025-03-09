namespace Product.Query.Application.GetByName;

public class GetProductByNameQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

