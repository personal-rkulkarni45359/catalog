using System;
using Catalog.Entitles;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;


//to run docker mongo image
//docker run -d --rm --name mongo -p 27018:27017 -v mongodbdata:/data/db mongo
//sudo chmod 666 /var/run/docker.sock

namespace  Catalog.Repositories
{

public class MongoDbItemsRepository : IItemsRepository
{
    private const string DATABASE_NAME="catalog"; 
    private const string COLLECTION_NAME="items;";

    private readonly IMongoCollection<Item> itemsCollection; 
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
    public MongoDbItemsRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(DATABASE_NAME); 
        itemsCollection = database.GetCollection<Item>(COLLECTION_NAME);
        
    }

  public   Item GetItem(Guid id)
  {
      var filter = filterBuilder.Eq(item => item.ID,id);
      return itemsCollection.Find(filter).SingleOrDefault();
   }

      public   IEnumerable<Item> GetItems()
      {
          
             return itemsCollection.Find(new BsonDocument()).ToList();
           

                
      }

      public   void CreateItem (Item item)
      {
          itemsCollection.InsertOne(item); 
      }

     public    void UpdateItem(Item item)
     {
         var replaceFilter = Builders<Item>.Filter.Eq(item1=> item1.ID, item.ID);
         itemsCollection.ReplaceOne(replaceFilter,item);
     }

       public   void DeleteItem(Guid id)
       {
        var deleteFilter = Builders<Item>.Filter.Eq(item=> item.ID, id);
        itemsCollection.DeleteOne(deleteFilter);

       }


}



}