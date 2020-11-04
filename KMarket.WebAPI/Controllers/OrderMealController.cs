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
    public class OrderMealController : ApiController
    {

        //creates a service to be used via function calls
        private OrderMealService CreateOrderMealService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var orderMealService = new OrderMealService(userId);
            return orderMealService;
        }

        //get-all orders in orders database
        public IHttpActionResult Get()
        {
            OrderMealService orderMealService = CreateOrderMealService();
            var orders = orderMealService.GetOrderMeals();
            return Ok(orders);
        }

        //post (create) new orders to orders database
        public IHttpActionResult Post(OrderMealCreate order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderMealService();

            if (!service.CreateOrderMeal(order))
                return InternalServerError();

            return Ok();
        }

        //gets a orders by id
        public IHttpActionResult Get(int id)
        {
            OrderMealService orderMealService = CreateOrderMealService();
            var order = orderMealService.GetOrderMealByID(id);
            return Ok(order);
        }

        //updates a orders by ID (name, price, desc, ingr)
        public IHttpActionResult Put(OrderMealEdit order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderMealService();

            if (!service.UpdateOrderMeal(order))
                return InternalServerError();

            return Ok();
        }

        //deletes orders by ID
        public IHttpActionResult Delete(int id)
        {
            var service = CreateOrderMealService();

            if (!service.DeleteOrderMeal(id))
                return InternalServerError();

            return Ok();
        }


    }

}