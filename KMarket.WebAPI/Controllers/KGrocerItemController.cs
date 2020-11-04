using KMarket.Models;
using KMarket.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KMarket.WebAPI.Controllers
{
    public class KGrocerItemController : ApiController
    {//creates a service to be used via function calls
        private KGrocerItemService CreateKGrocerItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var kGrocerItemService = new KGrocerItemService(userId);
            return kGrocerItemService;
        }

        //get-all kgrocer items in kgrocer database
        public IHttpActionResult Get()
        {
            KGrocerItemService kGrocerItemService = CreateKGrocerItemService();
            var meals = kGrocerItemService.GetKGrocerItems();
            return Ok(meals);
        }

        //post (create) new kgrocer item to kgrocer database
        public IHttpActionResult Post(KGrocerItemCreate kGrocerItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateKGrocerItemService();

            if (!service.CreateKGrocerItem(kGrocerItem))
                return InternalServerError();

            return Ok();
        }

        //gets a kgrocer item by id
        public IHttpActionResult Get(int id)
        {
            KGrocerItemService kGrocerItemService = CreateKGrocerItemService();
            var item = kGrocerItemService.GetKGrocerItemByID(id);
            return Ok(item);
        }

        //updates a kgrocer item by ID (name, price, desc, category)
        public IHttpActionResult Put(KGrocerItemEdit item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateKGrocerItemService();

            if (!service.UpdateKGrocerItem(item))
                return InternalServerError();

            return Ok();
        }

        //deletes kgrocer item by ID
        public IHttpActionResult Delete(int id)
        {
            var service = CreateKGrocerItemService();

            if (!service.DeleteKGrocerItem(id))
                return InternalServerError();

            return Ok();
        }


    }

}
