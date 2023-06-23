﻿using System;
using RichsSnackRack.Orders.Models.Enums;

namespace RichsSnackRack.Orders
{
	public sealed record OrderDetailStatus: Enumeration
	{
        public OrderDetailStatus(int id, string name, string description) : base(id, name, description)
        {

        }
        public static OrderDetailStatus Completed = new(id: (int)OrderStatus.Completed, name: nameof(OrderStatus.Completed), description: "Snack was sold");
        public static OrderDetailStatus Refunded = new(id: (int)OrderStatus.Refunded, name: nameof(OrderStatus.Refunded), description: "Customer wanted their money back for snack after it was given");
        public static OrderDetailStatus Cancelled = new(id: (int)OrderStatus.Cancelled, name: nameof(OrderStatus.Cancelled), description: "Customer asked for money back before item was prepared");

        public static OrderDetailStatus[] All = new OrderDetailStatus[] {
            Completed, Refunded, Cancelled
        };
    }
}

