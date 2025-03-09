using AutoMapper;

namespace Product.Command.Application.Delete
{
    public class DeleteProductProfile : Profile
    {
        public DeleteProductProfile()
        {
            CreateMap<DeleteProductCommand, ProductCommandDomainEntities.Product>();
            CreateMap<ProductCommandDomainEntities.Product, DeleteProductResult>();
        }
    }
}
