using System.Linq;
using System.Collections.Generic;
using Catalog.Entitles;
using System;


namespace Catalog.Repositories
{
    public class InMemItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item  { ID=Guid.NewGuid(), Name="Potion", Price=9, CreatedDate =DateTimeOffset.UtcNow} ,
            new Item  { ID=Guid.NewGuid(), Name="Iron Sword", Price=20, CreatedDate =DateTimeOffset.UtcNow} ,
            new Item  { ID=Guid.NewGuid(), Name="Broze Sheild", Price=18, CreatedDate =DateTimeOffset.UtcNow} 

        };

        public IEnumerable<Item> GetItems()
        {
            return items; 
        }

        public Item GetItem(Guid id )
        {
            return  items.Where(item => item.ID == id).SingleOrDefault();
        }
    }
}