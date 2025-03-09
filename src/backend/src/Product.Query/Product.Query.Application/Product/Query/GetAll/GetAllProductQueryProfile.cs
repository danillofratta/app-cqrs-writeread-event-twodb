using AutoMapper;

namespace Product.Query.Application.GetAll
{
    public class GetAllProductQueryProfile : Profile
    {
        public GetAllProductQueryProfile()
        {
            CreateMap<ProductQueryDomainEntities.Product, GetAllProductQueryResult>();
        }
    }
}
