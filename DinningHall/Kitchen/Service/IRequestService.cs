using System.Threading.Tasks;
using Kitchen.Models;

namespace Kitchen.Service
{
    public interface IRequestService
    {
        Task SendReadyOrder(Order order);
    }
}