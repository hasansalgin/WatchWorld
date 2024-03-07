using Web.Models;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetBasketViewModelAsync();
        public Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity);
    }
}
