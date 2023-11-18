using AutoMapper;
using CartingService.Domain.Commands.V1.UpdateCartItem;
using CartingService.Domain.Models.V1;
using CartingService.Infrastructure.Models.V1;
using OnlineStore.Contracts;

namespace CartingService.Infrastructure.AutoMapper;

public class AutoMapperProfileInfrastructure : Profile
{
    public AutoMapperProfileInfrastructure()
    {
        CreateMap<CartEntity, Cart>();
        CreateMap<Cart, CartEntity>();
        CreateMap<CartItemEntity, CartItem>();
        CreateMap<CartItem, CartItemEntity>();
        CreateMap<Models.V2.CartItemEntity, Domain.Models.V2.CartItem>();
        CreateMap<Domain.Models.V2.CartItem, Models.V2.CartItemEntity>();
        CreateMap<ProductMessage, UpdateCartItemCommand>();
    }
}