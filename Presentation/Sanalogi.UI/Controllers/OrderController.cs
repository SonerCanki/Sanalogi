using Microsoft.AspNetCore.Mvc;
using Sanalogi.Api.Models;
using Sanalogi.UI.APIs;
using System;
using System.Threading.Tasks;

namespace Sanalogi.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderApi _orderApi;

        public OrderController(IOrderApi orderApi)
        {
            _orderApi = orderApi;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _orderApi.GetOrder(id);
            Sanalogi.UI.Models.OrderModel model = new Sanalogi.UI.Models.OrderModel();
            if (result.Content.ResultData!=null)
            {
                model.Id = result.Content.ResultData.Id;
                model.OrderDate = result.Content.ResultData.OrderDate;
                model.OrderingCompany= result.Content.ResultData.OrderingCompany;
                model.OrderName= result.Content.ResultData.OrderName;
                model.Price= result.Content.ResultData.Price;
                model.Quantity= result.Content.ResultData.Quantity;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var insertResult = await _orderApi.Post(model);
                if (insertResult.IsSuccessStatusCode && insertResult.Content.IsSuccess && insertResult.Content.ResultData != null)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Kayıt işlemi sırasında bir hata oluştu!...Lütfen tüm alanları kontrol edip tekrar deneyiniz...";
            }
            TempData["Message"] = "İşlem başarısız oldu!...Lütfen tüm alanları kontrol edip tekrar deneyiniz...";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id, OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = await _orderApi.Put(id, model);
                if (updateResult.IsSuccessStatusCode && updateResult.Content.IsSuccess && updateResult.Content.ResultData != null)
                    return RedirectToAction("Index");
                else
                    TempData["Message"] = "Kayıt işlemi sırasında bir hata oluştu!...Lütfen tüm alanları kontrol edip tekrar deneyiniz...";
            }
            TempData["Message"] = "İşlem başarısız oldu!...Lütfen tüm alanları kontrol edip tekrar deneyiniz...";

            return RedirectToAction("Get",new { id=id});
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            var deletedResut = await _orderApi.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
