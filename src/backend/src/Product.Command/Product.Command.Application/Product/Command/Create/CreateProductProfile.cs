using AutoMapper;

namespace Product.Command.Application.Create
{
    public class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {

            CreateMap<CreateProductCommand, ProductCommandDomainEntities.Product>();

            CreateMap<ProductCommandDomainEntities.Product, CreateProductResult>();
        }
    }
}
