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
    public class OrderItemController : ApiController
    {//creates a service to be used via function calls
        private OrderItemService CreateOrderItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var orderItemService = new OrderItemService(userId);
            return orderItemService;
        }

        //get-all orders in order database
        public IHttpActionResult Get()
        {
            OrderItemService orderItemService = CreateOrderItemService();
            var orders = orderItemService.GetOrderItems();
            return Ok(orders);
        }

        //post (create) new order to order database
        public IHttpActionResult Post(OrderItemCreate order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderItemService();

            if (!service.CreateOrderItem(order))
                return InternalServerError();

            return Ok();
        }

        //gets a order by id
        public IHttpActionResult Get(int id)
        {
            OrderItemService orderItemService = CreateOrderItemService();
            var order = orderItemService.GetOrderItemByID(id);
            return Ok(order);
        }

        //updates a order by ID (name, price, desc, category)
        public IHttpActionResult Put(OrderItemEdit order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderItemService();

            if (!service.UpdateItemOrder(order))
                return InternalServerError();

            return Ok();
        }

        //deletes order by ID
        public IHttpActionResult Delete(int id)
        {
            var service = CreateOrderItemService();

            if (!service.DeleteOrderItem(id))
                return InternalServerError();

            return Ok();
        }


    }

}
