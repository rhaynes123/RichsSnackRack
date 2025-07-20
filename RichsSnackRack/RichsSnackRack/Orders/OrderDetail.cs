using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RichsSnackRack.Orders
{
    public readonly record struct OrderId
    {
        public Guid Value { get; }
        public OrderId(Guid Id)
        {
            if(Id == Guid.Empty || Id.Equals(new Guid()))
            {
                ArgumentException.ThrowIfNullOrEmpty(nameof(Id));
            }
            Value = Id;   
        }
    }
	public sealed record OrderDetail
	{
        [Key]
        public OrderId Id { get; init; }
        [Required(AllowEmptyStrings = false), StringLength(100)]
        public required string Name { get; init; }
        [Required, Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal Price { get; init; }
        public required OrderDetailStatus OrderStatus { get; init; }
        public required decimal OrderTotal { get; init; }
        [Required]
        public DateTime OrderDate { get; init; }
    }
}

