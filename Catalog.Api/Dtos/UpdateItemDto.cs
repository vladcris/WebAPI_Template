using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Dtos
{
    public record UpdateItemDto
    {
        [Required]
        public string? Name { get; init; }
        [Required]
        public decimal Price { get; init; }
    }
}