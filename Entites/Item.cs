using System;
using MongoDB.Bson;

namespace Catalog.Entitles 
{
    public record Item
    {

        public ObjectId _id {get;set;}
        public Guid ID{get; init; }

        public String Name {get; init;}

        public decimal Price {get; init;}

        public DateTimeOffset CreatedDate {get; init;}
    }
}