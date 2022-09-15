using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sanalogi.Api.Models;
using Sanalogi.Data.Entity;
using Sanalogi.Service.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sanalogi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("/order/{id}")]
        public async Task<ActionResult<WebApiResponse<OrderModel>>> GetOrder(Guid id)
        {
            var order = await _orderService.GetById(id);
            OrderModel model = new OrderModel();
            if (order != null)
            {
                model.Id = order.Id;
                model.OrderDate = order.OrderDate;
                model.OrderingCompany = order.OrderingCompany;
                model.OrderName = order.OrderName;
                model.Price = order.Price;
                model.Quantity = order.Quantity;
                return new WebApiResponse<OrderModel>(true, "Succes", model);
            }
            return new WebApiResponse<OrderModel>(true, "Error");
        }


        [HttpGet("/orders")]
        public async Task<ActionResult<WebApiResponse<List<OrderModel>>>> GetOrders()
        {
            List<OrderModel> orders = new List<OrderModel>();

            foreach (var item in await _orderService.Table.ToListAsync())
            {
                orders.Add(new OrderModel
                {
                    Id = item.Id,
                    OrderingCompany = item.OrderingCompany,
                    OrderDate = item.OrderDate,
                    OrderName = item.OrderName,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }
            if (orders.Count > 0)
            {
                return new WebApiResponse<List<OrderModel>>(true, "Succes", orders);
            }
            return new WebApiResponse<List<OrderModel>>(true, "Error");
        }

        [HttpPost("/order")]
        public async Task<ActionResult<WebApiResponse<Order>>> PostOrder(OrderModel request)
        {
            Order entity = new Order()
            {
                OrderName = request.OrderName,
                OrderingCompany = request.OrderingCompany,
                OrderDate = request.OrderDate,
                Price = request.Price,
                Quantity = request.Quantity
            };

            var insertResult = await _orderService.Add(entity);

            if (insertResult != null)
            {
                return new WebApiResponse<Order>(true, "Succes", insertResult);
            }

            return new WebApiResponse<Order>(false, "Error");
        }

        [HttpPut("/order/{id}")]
        public async Task<ActionResult<WebApiResponse<OrderModel>>> PutOrder(Guid id, OrderModel request)
        {
            if (id != request.Id)
                return BadRequest();
            try
            {
                Order entity = await _orderService.GetById(id);
                if (entity == null)
                    return NotFound();

                entity.Id = request.Id;
                entity.OrderDate = request.OrderDate;
                entity.OrderingCompany = request.OrderingCompany;
                entity.OrderName = request.OrderName;
                entity.Price = request.Price;
                entity.Quantity = request.Quantity;

                var updateResult = await _orderService.Update(entity);
                if (updateResult != null)
                {
                    return new WebApiResponse<OrderModel>(true, "Success", request);
                }
                return new WebApiResponse<OrderModel>(false, "Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("/order/{id}")]
        public async Task<ActionResult<WebApiResponse<Order>>> DeleteCategory(Guid id)
        {
            Order entity = await _orderService.GetById(id);
            if (entity != null)
            {
                if (await _orderService.Remove(entity))
                {
                    return new WebApiResponse<Order>(true, "Success", entity);
                }
                else
                    return new WebApiResponse<Order>(false, "Error");
            }
            return new WebApiResponse<Order>(false, "Error");
        }

    }
}
