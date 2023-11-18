using AutoMapper;
using CartingService.Domain.Commands.V1.CreateCart;
using CartingService.Domain.Commands.V1.UpdateCartItem;
using CartingService.Domain.Models.V1;

namespace CartingService.Domain.AutoMapper;

public class AutoMapperProfileDomain : Profile
{
    public AutoMapperProfileDomain()
    {
        CreateMap<CreateCartCommand, CartItem>();
        CreateMap<UpdateCartItemCommand, CartItem>();
    }
}