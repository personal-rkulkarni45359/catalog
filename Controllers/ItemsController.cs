using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entitles;

namespace catalog.Controllers
{
    [ApiController]
    [Route("items")]
     public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository respostiory;

        public ItemsController()
        {
            respostiory = new InMemItemsRepository(); 
        }


        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            return respostiory.GetItems();
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item= respostiory.GetItem( id); 

            if(item is null)
            {
                return NotFound();
            }
            return item;
        }


    }


}