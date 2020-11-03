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
    public class OrderController : ApiController
    {

        //creates a service to be used via function calls
        private OrderService CreateOrderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var orderService = new OrderService(userId);
            return orderService;
        }

        //get-all orders in orders database
        public IHttpActionResult Get()
        {
            OrderService orderService = CreateOrderService();
            var orders = orderService.GetOrders();
            return Ok(orders);
        }

        //post (create) new orders to orders database
        public IHttpActionResult Post(OrderCreate order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderService();

            if (!service.CreateOrder(order))
                return InternalServerError();

            return Ok();
        }

        //gets a orders by id
        public IHttpActionResult Get(int id)
        {
            OrderService orderService = CreateOrderService();
            var order = orderService.GetOrderByID(id);
            return Ok(order);
        }

        //updates a orders by ID (name, price, desc, ingr)
        public IHttpActionResult Put(OrderEdit order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderService();

            if (!service.UpdateOrder(order))
                return InternalServerError();

            return Ok();
        }

        //deletes orders by ID
        public IHttpActionResult Delete(int id)
        {
            var service = CreateOrderService();

            if (!service.DeleteOrder(id))
                return InternalServerError();

            return Ok();
        }


    }

}