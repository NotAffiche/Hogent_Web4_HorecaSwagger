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
    private IOrderRepository oRepo;

    public OrderService(IOrderRepository orepo)
    {
        oRepo = orepo;
    }

    public void Create(Order o, DishService ds)
    {
        oRepo.CreateOrder(o);
        if (o.PaymentDate!=null) SubtractAmountAvailableDishes(o, ds);
    }

    public Order Read(int id)
    {
        return oRepo.Read(id);
    }

    public ICollection<Order> ReadOrdersByCustomer(int customerId)
    {
        return oRepo.ReadOrdersByCustomer(customerId);
    }

    public ICollection<Order> ReadAll()
    {
        return oRepo.ReadAll();
    }

    public void Update(Order o, DishService ds)
    {
        oRepo.UpdateOrder(o);
        if (o.PaymentDate != null) SubtractAmountAvailableDishes(o, ds);
    }

    public void Delete(int id)
    {
        oRepo.DeleteOrder(id);
    }

    private void SubtractAmountAvailableDishes(Order o, DishService ds)
    {
        foreach (var dishWithAmount in o.DishesWithAmount)
        {
            foreach (var dwa in dishWithAmount)
            {
                Dish d = dwa.Key;
                int amount = dwa.Value;
                d.AmountAvailable -= amount;
                ds.Update(d);
            }
        }
    }
}
