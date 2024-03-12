using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await _basketViewModelService.GetBasketViewModelAsync();
            return View(basket);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int Quantity = 1)
        {
            var basket = await _basketViewModelService.AddItemToBasketAsync(productId, Quantity);
            return Json(basket);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Empty()
        {
            await _basketViewModelService.EmptyBasketAsync();
            TempData["Message"] = "Your basket is now empty.";
            return RedirectToAction("Index");

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            await _basketViewModelService.RemoveItemAsync(productId);
            TempData["Message"] = "Item removed from the basket.";
            return RedirectToAction("Index");

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([ModelBinder(Name = "quantities")] Dictionary<int, int> quantities)
        {
            await _basketViewModelService.SetQuantitiesAsync(quantities);
            TempData["Message"] = "Basket updated.";

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketViewModelService.GetBasketViewModelAsync();
            var vm = new CheckoutViewModel() { Basket = basket };
            return View(vm);
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _basketViewModelService.CheckoutAsync(vm.Street, vm.City, vm.State, vm.Country, vm.ZipCode);
                return RedirectToAction("OrderConfirmed");
            }

            var basket = await _basketViewModelService.GetBasketViewModelAsync();
            vm.Basket = basket;
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> OrderConfirmed()
        {
            return View();
        }
    }
}
