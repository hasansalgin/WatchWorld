﻿using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int Quantity = 1)
        {
            var basket = await _basketViewModelService.AddItemToBasketAsync(productId, Quantity);
            return Json(basket);
        }
    }
}