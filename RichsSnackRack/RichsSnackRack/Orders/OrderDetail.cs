using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RichsSnackRack.Orders
{
	public sealed record OrderDetail
	{
        [Key]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false), StringLength(100)]
        public required string Name { get; set; }
        [Required, Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal Price { get; set; }
        public required OrderDetailStatus OrderStatus { get; set; }
        public required decimal OrderTotal { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
    }
}

