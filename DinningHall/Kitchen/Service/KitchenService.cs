using System.Threading.Tasks;
using Kitchen.Domain.Repository;
using Kitchen.Models;
using Kitchen.Service;

namespace DinningHall.Service
{
    public class KitchenService : IKitchenService
    {
        private readonly IBaseRepository _baseRepository;
        
        public KitchenService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        
        public async Task ReceiveOrder(Order order)
        {
            if (order is object)
                await _baseRepository.AddOrder(order);
        }
    }
}