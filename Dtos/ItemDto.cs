using System;
namespace Catalog.Dtos
{
    public record ItemDto
    {
        public Guid ID{get; init; }

        public String Name {get; init;}

        public decimal Price {get; init;}

        public DateTimeOffset CreatedDate {get; init;}
    }


}