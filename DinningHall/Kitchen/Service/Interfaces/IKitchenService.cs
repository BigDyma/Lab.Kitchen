using System.Threading.Tasks;
using Kitchen.Models;

namespace DinningHall.Service
{
    public interface IKitchenService
    {
        Task ReceiveOrder(Order order);
    }
}