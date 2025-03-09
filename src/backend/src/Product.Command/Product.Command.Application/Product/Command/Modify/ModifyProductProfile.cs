using AutoMapper;

namespace Product.Command.Application.Modify
{
    public class ModifyProductProfile : Profile
    {
        public ModifyProductProfile()
        {
            CreateMap<ModifyProductCommand, ProductCommandDomainEntities.Product>();
            CreateMap<ProductCommandDomainEntities.Product, ModifyProductResult>();
        }
    }
}
