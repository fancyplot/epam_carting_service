﻿namespace CartingService.Infrastructure.Dto.V1;

public class CartEntityDto
{
    public int CartId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}