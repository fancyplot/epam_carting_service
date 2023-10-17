using AutoMapper;
using CartingService.Domain.Commands.V1.CreateCart;
using CartingService.Domain.Models.V1;

namespace CartingService.API.AutoMapper;

public class AutoMapperProfileApi : Profile
{
    public AutoMapperProfileApi()
    {
        CreateMap<CreateCartCommand, Cart>();
    }
}