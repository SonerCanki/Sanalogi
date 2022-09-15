using Microsoft.AspNetCore.Mvc;
using Refit;
using Sanalogi.Api.Models;
using Sanalogi.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sanalogi.UI.APIs
{
    public interface IOrderApi
    {
        [Post("/order")]
        Task<ApiResponse<WebApiResponse<Order>>> Post(OrderModel request);

        [Get("/order/{id}")]
        Task<ApiResponse<WebApiResponse<OrderModel>>> GetOrder(Guid id);

        [Get("/orders")]
        Task<ApiResponse<WebApiResponse<List<OrderModel>>>> GetOrders();

        [Put("/order/{id}")]
        Task<ApiResponse<WebApiResponse<OrderModel>>> Put(Guid id, OrderModel request);

        [Delete("/order/{id}")]
        Task<ApiResponse<WebApiResponse<Order>>> Delete(Guid id);

    }
}
