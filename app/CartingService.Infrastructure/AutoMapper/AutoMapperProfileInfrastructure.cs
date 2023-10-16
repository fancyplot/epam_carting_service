using AutoMapper;
using CartingService.Domain.Models.V1;
using CartingService.Infrastructure.Dto.V1;
using CartingService.Infrastructure.Models.V1;

namespace CartingService.Infrastructure.AutoMapper;

public class AutoMapperProfileInfrastructure : Profile
{
    public AutoMapperProfileInfrastructure()
    {
        CreateMap<CartEntity, Cart>();
        CreateMap<CartEntityDto, CartEntity>();
    }
}