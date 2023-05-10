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
        ctx.Orders.Add(oEF);
        SaveAndClear();
        foreach (var dict in order.DishesWithAmount)
        {
            foreach (var item in dict)
            {
                oEF.Dishes.Add(ctx.Dishes.Find(item.Key.DishUUID));
                ctx.OrderDetails.Add(new OrderDetailsEF() { OrderUUID = oEF.OrderUUID, DishUUID = item.Key.DishUUID/*ctx.Dishes.Find(item.Key.DishUUID).DishUUID*/, DishAmount = item.Value });
            }
        }
        SaveAndClear();
    }

    public void DeleteOrder(Order order)
    {
        var oEF = ctx.Orders.Find(order.OrderUUID);
        oEF.Deleted = true;
        ctx.Orders.Update(oEF);
        SaveAndClear();
    }

    public Order Read(int id)
    {
        OrderEF oEF = ctx.Orders.Where(x => x.Deleted == false&&x.OrderUUID==id).AsNoTracking().SingleOrDefault();
        Order order = null;
        List<Dictionary<Dish, int>> dishesWithAmount = new List<Dictionary<Dish, int>>();
        foreach (var od in ctx.OrderDetails.Where(x => x.OrderUUID == oEF.OrderUUID).AsNoTracking().ToList())
        {
            dishesWithAmount.Add(new Dictionary<Dish, int> { { DishMapper.MapToDomain(ctx.Dishes.Where(x => x.DishUUID == od.DishUUID).AsNoTracking().SingleOrDefault()), od.DishAmount } });
        }
        var customerEF = ctx.Customers.Where(x => x.CustomerUUID == oEF.CustomerUUID).AsNoTracking().SingleOrDefault();
        order =new Order(oEF.OrderUUID, oEF.CreateDate, oEF.PaymentDate, CustomerMapper.MapToDomain(customerEF), dishesWithAmount);
        return order;
    }

    public ICollection<Order> ReadAll()
    {
        List<OrderEF> ordersEFs = ctx.Orders.Where(x => x.Deleted == false).AsNoTracking().ToList();
        List<Order> orders = new List<Order>();
        foreach (var oEF in ordersEFs)
        {
            List<Dictionary<Dish, int>> dishesWithAmount = new List<Dictionary<Dish, int>>();
            foreach (var od in ctx.OrderDetails.Where(x=>x.OrderUUID==oEF.OrderUUID).AsNoTracking().ToList())
            {
                dishesWithAmount.Add(new Dictionary<Dish, int> { { DishMapper.MapToDomain(ctx.Dishes.Where(x => x.DishUUID == od.DishUUID).AsNoTracking().SingleOrDefault()), od.DishAmount } });
            }
            var customerEF = ctx.Customers.Where(x=>x.CustomerUUID==oEF.CustomerUUID).AsNoTracking().SingleOrDefault(); 
            orders.Add(new Order(oEF.OrderUUID, oEF.CreateDate, oEF.PaymentDate, CustomerMapper.MapToDomain(customerEF), dishesWithAmount));
        }
        return orders;
    }

    public void UpdateOrder(Order order)
    {
        var oEF = ctx.Orders.Find(order.OrderUUID);

        oEF.OrderUUID = order.OrderUUID;
        oEF.CreateDate = order.CreateDate;
        oEF.PaymentDate = order.PaymentDate;
        oEF.Deleted = false;
        oEF.Customer = ctx.Customers.Find(order.Customer.CustomerUUID);

        foreach (var dict in order.DishesWithAmount)
        {
            foreach (var item in dict)
            {
                oEF.Dishes.Add(ctx.Dishes.Find(item.Key.DishUUID));
                OrderDetailsEF odEF = ctx.OrderDetails.Where(x=>x.OrderUUID==oEF.OrderUUID&&x.DishUUID==item.Key.DishUUID).AsNoTracking().SingleOrDefault();
                odEF.DishAmount = item.Value;
                ctx.OrderDetails.Update(odEF);
            }
        }

        ctx.Orders.Update(oEF);
        SaveAndClear();
    }
}
