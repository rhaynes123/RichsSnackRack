using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RichsSnackRack.Menu.Models
{
	public sealed class Snack
    {
        public Snack()
        {
        }
        [Key]
		public int Id { get; set; }
        [Required(AllowEmptyStrings = false), StringLength(100)]
        public required string Name { get; set; }
        [Required,Range(0, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public bool Active { get; set; } = true;
    }
}

