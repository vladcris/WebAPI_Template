using System;

namespace Catalog.Api.Entities
{
    public record Item  // records
    {
       public Guid Id { get; init; }  // init-only properties
       public string? Name { get; init; }
       public decimal Price { get; init; }
       public DateTimeOffset CreatedDate { get; init; } // DatetimeOffset
    }
}