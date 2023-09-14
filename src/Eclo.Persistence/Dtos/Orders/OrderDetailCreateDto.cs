﻿namespace Eclo.Persistence.Dtos.Orders;

public class OrderDetailCreateDto
{
    public long OrderId { get; set; }

    public long ProductDiscountId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }
}
