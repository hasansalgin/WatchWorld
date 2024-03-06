using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetOrCreateBasketAsync(string buyerId);
        Task<Basket> GetOrBasketAsync(string buyerId);
        Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity);
        Task<Basket> SetQuantitiesAsync(string buyerId, Dictionary<int, int> quantities);

        Task DeleteBasketItemAsync(string buyerId, int productId);

        Task emptyBasketAsync(string buyerId);

        Task TransferBasketAsync(string buyerId, string destinationBuyerId);
    }
}
