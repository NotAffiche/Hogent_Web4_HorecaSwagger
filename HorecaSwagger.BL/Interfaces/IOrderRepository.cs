using HorecaSwagger.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Interfaces;

public interface IOrderRepository
{
    void CreateOrder(Order order);
    Order Read(int id);
    ICollection<Order> ReadAll();
    ICollection<Order> ReadOrdersByCustomer(int customerId);
    void UpdateOrder(Order order);
    void DeleteOrder(int id);
}
