using AutoMapper;


namespace Product.Query.Application.GetByName
{
    public class GetProductByNameQueryProfile : Profile
    {
        public GetProductByNameQueryProfile()
        {
            CreateMap<ProductQueryDomainEntities.Product, GetProductByNameQueryResult>();
        }
    }
}
