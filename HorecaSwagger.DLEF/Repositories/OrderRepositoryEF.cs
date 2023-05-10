using HorecaSwagger.BL.Interfaces;
using HorecaSwagger.BL.Model;
using HorecaSwagger.DLEF.Mappers;
using HorecaSwagger.DLEF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Repositories;

public class OrderRepositoryEF : IOrderRepository
{
    private DataContext ctx;

    public OrderRepositoryEF(string connStr)
    {
        ctx = new DataContext(connStr);
    }

    private void SaveAndClear()
    {
        ctx.SaveChanges();
        ctx.ChangeTracker.Clear();
    }

    public void CreateOrder(Order order)
    {
        var oEF = new OrderEF()
        {
            OrderUUID = order.OrderUUID,
            CreateDate = order.CreateDate,
            PaymentDate = order.PaymentDate,
            Deleted = false,
            Customer = ctx.Customers.Find(order.Customer.CustomerUUID),
        };
        foreach (var item in order.Dishes)
        {
            oEF.Dishes.Add(ctx.Dishes.Find(item.DishUUID));
        }
        ctx.Orders.Add(oEF);
        SaveAndClear();
    }

    public void DeleteOrder(Order order)
    {
        ctx.Orders.Update(OrderMapper.MapToDB(order, true));
        SaveAndClear();
    }

    public Order Read(int id)
    {
        return OrderMapper.MapToDomain(ctx.Orders.Where(x => x.OrderUUID == id && x.Deleted == false).AsNoTracking().SingleOrDefault());
    }

    public ICollection<Order> ReadAll()
    {
        List<OrderEF> ordersEFs = ctx.Orders.Where(x => x.Deleted == false).AsNoTracking().ToList();
        foreach (var item in ordersEFs)
        {
            List<DishEF> dishesEFs = new List<DishEF>();
            item.Customer = ctx.Customers.Where(x => x.CustomerUUID == item.CustomerUUID).AsNoTracking().SingleOrDefault();
            List<OrderDetailsEF> odEFs = ctx.OrderDetails.Where(x=>x.OrderUUID==item.OrderUUID).ToList();
            foreach (var od in odEFs)
            {
                for (int i = 0; i < od.DishAmount; i++)
                {
                    item.Dishes.Add(ctx.Dishes.Find(od.DishUUID));
                }
            }
        }
        Console.WriteLine(ordersEFs);
        return ordersEFs.Select(x => OrderMapper.MapToDomain(x)).ToList();
    }

    public void UpdateOrder(Order order)
    {
        ctx.Orders.Update(OrderMapper.MapToDB(order, false));
        SaveAndClear();
    }
}
