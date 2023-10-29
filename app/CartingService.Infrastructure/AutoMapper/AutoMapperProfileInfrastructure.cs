﻿using AutoMapper;
using CartingService.Domain.Models.V1;
using CartingService.Infrastructure.Models.V1;

namespace CartingService.Infrastructure.AutoMapper;

public class AutoMapperProfileInfrastructure : Profile
{
    public AutoMapperProfileInfrastructure()
    {
        CreateMap<CartEntity, Cart>();
        CreateMap<Cart, CartEntity>();
        CreateMap<CartItemEntity, CartItem>();
        CreateMap<CartItem, CartItemEntity>();
    }
}