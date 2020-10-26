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
    public class KCafeMealController : ApiController
    {

        //creates a service to be used via function calls
        private KCafeMealService CreateKCafeMealService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var kCafeMealService = new KCafeMealService(userId);
            return kCafeMealService;
        }

        //get-all kmeals in kmeals database
        public IHttpActionResult Get()
        {
            KCafeMealService kCafeMealService = CreateKCafeMealService();
            var meals = kCafeMealService.GetKCafeMeals();
            return Ok(meals);
        }

        //post (create) new kmeal to kmeals database
        public IHttpActionResult Post(KCafeMealCreate kCafeMeal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateKCafeMealService();

            if (!service.CreateKCafeMeal(kCafeMeal))
                return InternalServerError();

            return Ok();
        }

        //gets a kmeal by id
        public IHttpActionResult Get(int id)
        {
            KCafeMealService kCafeMealService = CreateKCafeMealService();
            var meal = kCafeMealService.GetKCafeMealByID(id);
            return Ok(meal);
        }

        //updates a meal by ID (name, price, desc, ingr)
        public IHttpActionResult Put(KCafeMealEdit meal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateKCafeMealService();

            if (!service.UpdateKCafeMeal(meal))
                return InternalServerError();

            return Ok();
        }

        //deletes meal by ID
        public IHttpActionResult Delete(int id)
        {
            var service = CreateKCafeMealService();

            if (!service.DeleteKCafeMeal(id))
                return InternalServerError();

            return Ok();
        }


    }

}