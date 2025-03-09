using AutoMapper;


namespace Product.Query.Application.GetById
{
    public class GetProductByIdQueryProfile : Profile
    {
        public GetProductByIdQueryProfile()
        {
            CreateMap<ProductQueryDomainEntities.Product, GetProductByIdQueryResult>();
        }
    }
}
