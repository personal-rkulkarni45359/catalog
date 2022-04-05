using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Dtos;
using Catalog.Entitles;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
     public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository respostiory;

        public ItemsController(IItemsRepository repository)
        {
            this.respostiory = repository; 
        }


        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items=respostiory.GetItems().Select(item => item.AsDto());
                
            return items;
        } 

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = respostiory.GetItem( id);

            if(item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        //postw with items 
        [HttpPost]
        public ActionResult<ItemDto>CreateItem(CreateItemDto itemDto)
        {
            Item item = new ()
            {
                ID = Guid.NewGuid(), 
                Name =itemDto.Name,
                Price = itemDto.Price,
                CreatedDate  = DateTimeOffset.UtcNow
            };

            respostiory.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new {id = item.ID}, item.AsDto() );

        }

        // put into /items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var exsitingItem = respostiory.GetItem(id); 
            
            if(exsitingItem is null)
            {
                return NotFound(); 
            }

            Item updatedItem = exsitingItem with {

                Name = itemDto.Name,
                Price = itemDto.Price

            };

            respostiory.UpdateItem(updatedItem); 

            return NoContent(); 
        }

        //DELETE /items/ {id}
        [HttpDelete("{id}")]

        public ActionResult DeleteItem(Guid id) 
        {
             var exsitingItem = respostiory.GetItem(id); 
            
            if(exsitingItem is null)
            {
                return NotFound(); 
            }

            respostiory.DeleteItem(id); 

            return NoContent(); 


        }


    }


}