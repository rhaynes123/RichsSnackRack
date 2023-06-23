using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RichsSnackRack.Orders.Models.Enums;

namespace RichsSnackRack.Orders.Models
{
	public sealed class Order
	{
		public Order()
		{
		}
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required, Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
		public required int SnackId { get; set; }
		public required OrderStatus OrderStatus { get; set; } = OrderStatus.Completed;
		[Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}

