using HorecaSwagger.BL.Interfaces;
using HorecaSwagger.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Services;

public class OrderService
{
    private IOrderRepository repo;

    public OrderService(IOrderRepository repo)
    {
        this.repo = repo;
    }

    public void Create(Order o)
    {
        repo.CreateOrder(o);
    }

    public Order Read(int id)
    {
        return repo.Read(id);
    }

    public ICollection<Order> ReadOrdersByCustomer(int customerId)
    {
        return repo.ReadOrdersByCustomer(customerId);
    }

    public ICollection<Order> ReadAll()
    {
        return repo.ReadAll();
    }

    public void Update(Order o)
    {
        repo.UpdateOrder(o);
    }

    public void Delete(int id)
    {
        repo.DeleteOrder(id);
    }
}
