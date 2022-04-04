using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Dtos;

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


    }


}