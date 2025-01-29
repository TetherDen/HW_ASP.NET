using HW_21_Unit_Tests.Models;

namespace HW_21_Unit_Tests.Interfaces
{
    public interface IOrder
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        Order CreateOrder(Order order);
        bool DeleteOrder(int id);
    }
}
