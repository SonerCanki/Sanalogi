using Microsoft.AspNetCore.Mvc;
using Sanalogi.UI.APIs;
using Sanalogi.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sanalogi.UI.ViewComponents
{
    public class OrdersViewComponent : ViewComponent
    {
        private readonly IOrderApi _orderApi;

        public OrdersViewComponent(IOrderApi orderApi)
        {
            _orderApi = orderApi;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<OrderModel> orders = new List<OrderModel>();

            var list = await _orderApi.GetOrders();

            if (list.Content.ResultData != null)
            {
                foreach (var item in list.Content.ResultData)
                {
                    orders.Add(new OrderModel
                    {
                        Id = item.Id,
                        OrderDate = item.OrderDate,
                        OrderingCompany = item.OrderingCompany,
                        OrderName = item.OrderName,
                        Price = item.Price,
                        Quantity = item.Quantity
                    });
                }
            }
            return View(orders);
        }
    }
}
