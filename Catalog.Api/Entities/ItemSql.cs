using System;


namespace Catalog.Api.Entities
{
    public record ItemSql  // records
    {
       public string Id { get; init; }  // init-only properties
       public string? Name { get; init; }
       public decimal Price { get; init; }
       public DateTimeOffset CreatedDate { get; init; } // DatetimeOffset
    }
}