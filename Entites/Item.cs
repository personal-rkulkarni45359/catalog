using System;

namespace Catalog.Entitles 
{
    public record Item
    {
        public Guid ID{get; init; }

        public String Name {get; init;}

        public decimal Price {get; init;}

        public DateTimeOffset CreatedDate {get; init;}
    }
}