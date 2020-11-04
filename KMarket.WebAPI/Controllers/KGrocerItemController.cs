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
    [Authorize]
    public class KGrocerItemController : ApiController
    {

        private KGrocerItemService CreateKGrocerItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var kGrocerItemService = new KGrocerItemService(userId);
            return kGrocerItemService;
        }

        //get-all kitems in kitems database
        public IHttpActionResult Get()
        {
            KGrocerItemService kGrocerItemService = CreateKGrocerItemService();
            var items = kGrocerItemService.GetKGrocerItems();
            return Ok(items);
        }

        //post (create) new kitems to kitems database

        public IHttpActionResult Post(KGrocerItemCreate kGrocerItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateKGrocerItemService();

            if (!service.CreateKGrocerItem(kGrocerItem))
                return InternalServerError();

            return Ok();
        }

        //gets a kitems by id
        public IHttpActionResult Get(int id)
        {
            KGrocerItemService kGrocerItemService = CreateKGrocerItemService();
            var meal = kGrocerItemService.GetKGrocerItemByID(id);
            return Ok(meal);
        }

        //updates a kitems by ID (name, price, desc, ingr)

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
