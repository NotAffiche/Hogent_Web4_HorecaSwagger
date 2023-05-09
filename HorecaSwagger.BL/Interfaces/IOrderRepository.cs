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
    ICollection<Order> ReadAll();
    Order Read(int id);
    void UpdateOrder(Order order);
    void DeleteOrder(Order order);
}
