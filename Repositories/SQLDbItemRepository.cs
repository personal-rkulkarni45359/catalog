

using Catalog.Entitles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Repositories
{

public class SQLDbItemRespository : IItemsRepository
{

     private readonly SqlConnection cnn;
    public SQLDbItemRespository(SqlConnection cnn)
    {
    
             this.cnn=  cnn; 
             try
             {
             cnn.Open(); 
             }
             catch(Exception e)
             {
                 Console.Write(e); 
                 Environment.Exit(1); 
                 
                
             }
    }


        public void CreateItem(Item item)
    {
        throw new NotImplementedException();
    }

    public void DeleteItem(Guid id)
    {
        throw new NotImplementedException();
    }

    public Item GetItem(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Item> GetItems()
    {
        throw new NotImplementedException();
    }

    public void UpdateItem(Item item)
    {
        throw new NotImplementedException();
    }
}
}