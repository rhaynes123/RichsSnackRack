using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public int SnackId { get; set; }
		[Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}

