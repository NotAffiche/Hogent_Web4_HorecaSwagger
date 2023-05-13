using HorecaSwagger.BL.Interfaces;
using HorecaSwagger.BL.Model;
using HorecaSwagger.DLEF.Exceptions;
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
        try
        {
            var oEF = new OrderEF()
            {
                OrderUUID = order.OrderUUID,
                CreateDate = order.CreateDate,
                PaymentDate = order.PaymentDate,
                Deleted = false,
                Customer = ctx.Customers.Find(order.Customer.CustomerUUID)!,//only existing customers (found in db) get to make orders
            };
            List<OrderDetailsEF> odEFsToBeAdded = new();
            foreach (var dict in order.DishesWithAmount)
            {
                foreach (var item in dict)
                {
                    DishEF? d = ctx.Dishes.Find(item.Key.DishUUID);
                    if (d == null) throw new RepositoryException("Create Order - dish not found", new NullReferenceException());
                    if (d.AmountAvailable < item.Value) throw new RepositoryException($"Create Order - order too high (not enough available) [In Order: {item.Value} - AmountAvailable: {d.AmountAvailable}]", new ArgumentException());
                    //oEF.Dishes.Add(d);
                    odEFsToBeAdded.Add(new OrderDetailsEF() { OrderUUID = oEF.OrderUUID, DishUUID = item.Key.DishUUID/*ctx.Dishes.Find(item.Key.DishUUID).DishUUID*/, DishAmount = item.Value });
                }
            }
            ctx.Orders.Add(oEF);
            SaveAndClear();
            odEFsToBeAdded.ForEach(x=>x.OrderUUID= oEF.OrderUUID);
            ctx.OrderDetails.AddRange(odEFsToBeAdded);
            SaveAndClear();
        }
        catch(Exception ex)
        {
            throw new RepositoryException("Create Order", ex);
        }
    }

    public void DeleteOrder(int id)
    {
        try
        {
            OrderEF? oEF = ctx.Orders.Find(id);
            if (oEF == null) throw new RepositoryException("Delete Order - order not found", new NullReferenceException());
            oEF.Deleted = true;
            ctx.Orders.Update(oEF);
            SaveAndClear();
        }
        catch(Exception ex)
        {
            throw new RepositoryException("Delete Order", ex);
        }
    }

    public Order Read(int id)
    {
        try
        {
            OrderEF oEF = ctx.Orders.Where(x => x.Deleted == false && x.OrderUUID == id).AsNoTracking().SingleOrDefault();
            Order order = null;
            List<Dictionary<Dish, int>> dishesWithAmount = new List<Dictionary<Dish, int>>();
            foreach (var od in ctx.OrderDetails.Where(x => x.OrderUUID == oEF.OrderUUID).AsNoTracking().ToList())
            {
                dishesWithAmount.Add(new Dictionary<Dish, int> { { DishMapper.MapToDomain(ctx.Dishes.Where(x => x.DishUUID == od.DishUUID).AsNoTracking().SingleOrDefault()), od.DishAmount } });
            }
            var customerEF = ctx.Customers.Where(x => x.CustomerUUID == oEF.CustomerUUID).AsNoTracking().SingleOrDefault();
            order = new Order(oEF.OrderUUID, oEF.CreateDate, oEF.PaymentDate, CustomerMapper.MapToDomain(customerEF), dishesWithAmount);
            return order;
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Read Order {id}", ex);
        }
    }

    public ICollection<Order> ReadOrdersByCustomer(int customerId)
    {
        try
        {
            List<OrderEF> ordersEFs = ctx.Orders.Where(x => x.Deleted == false && x.Customer.CustomerUUID==customerId).AsNoTracking().ToList();
            if (ordersEFs == null || ordersEFs.Count == 0) throw new RepositoryException($"No Orders by Customer #{customerId}", new NullReferenceException());
            List<Order> orders = new List<Order>();
            foreach (var oEF in ordersEFs)
            {
                List<Dictionary<Dish, int>> dishesWithAmount = new List<Dictionary<Dish, int>>();
                foreach (var od in ctx.OrderDetails.Where(x => x.OrderUUID == oEF.OrderUUID).AsNoTracking().ToList())
                {
                    dishesWithAmount.Add(new Dictionary<Dish, int> { { DishMapper.MapToDomain(ctx.Dishes.Where(x => x.DishUUID == od.DishUUID).AsNoTracking().Single()), od.DishAmount } });
                }
                var customerEF = ctx.Customers.Where(x => x.CustomerUUID == oEF.CustomerUUID).AsNoTracking().Single();
                orders.Add(new Order(oEF.OrderUUID, oEF.CreateDate, oEF.PaymentDate, CustomerMapper.MapToDomain(customerEF), dishesWithAmount));
            }
            return orders;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Read Orders By Customer", ex);
        }
    }

    public ICollection<Order> ReadAll()
    {
        try
        {
            List<OrderEF> ordersEFs = ctx.Orders.Where(x => x.Deleted == false).AsNoTracking().ToList();
            List<Order> orders = new List<Order>();
            foreach (var oEF in ordersEFs)
            {
                List<Dictionary<Dish, int>> dishesWithAmount = new List<Dictionary<Dish, int>>();
                foreach (var od in ctx.OrderDetails.Where(x => x.OrderUUID == oEF.OrderUUID).AsNoTracking().ToList())
                {
                    dishesWithAmount.Add(new Dictionary<Dish, int> { { DishMapper.MapToDomain(ctx.Dishes.Where(x => x.DishUUID == od.DishUUID).AsNoTracking().Single()), od.DishAmount } });
                }
                var customerEF = ctx.Customers.Where(x => x.CustomerUUID == oEF.CustomerUUID).AsNoTracking().Single();
                orders.Add(new Order(oEF.OrderUUID, oEF.CreateDate, oEF.PaymentDate, CustomerMapper.MapToDomain(customerEF), dishesWithAmount));
            }
            return orders;
        }
        catch(Exception ex)
        {
            throw new RepositoryException("Read Orders", ex);
        }
    }
    public void UpdateOrder(Order order)
    {
        try
        {
            var oEF = ctx.Orders.Find(order.OrderUUID)!;

            oEF.OrderUUID = order.OrderUUID;
            oEF.CreateDate = order.CreateDate;
            oEF.PaymentDate = order.PaymentDate;
            oEF.Deleted = false;
            oEF.Customer = ctx.Customers.Find(order.Customer.CustomerUUID)!;

            var orderDetailsEFsfromDB = ctx.OrderDetails.Where(x => x.OrderUUID == oEF.OrderUUID).ToList();

            foreach (var dict in order.DishesWithAmount)
            {
                foreach (var item in dict)
                {
                    // check if the dish already has an od in the database
                    var existingOrderDetail = orderDetailsEFsfromDB.FirstOrDefault(x => x.DishUUID == item.Key.DishUUID);

                    //
                    DishEF? d = ctx.Dishes.Find(item.Key.DishUUID);
                    if (d.AmountAvailable < item.Value) throw new RepositoryException($"Create Order - order too high (not enough available) [In Order: {item.Value} - AmountAvailable: {d.AmountAvailable}]", new ArgumentException());
                    if (existingOrderDetail != null)
                    {
                        // update existing od
                        existingOrderDetail.DishAmount = item.Value;
                        ctx.OrderDetails.Update(existingOrderDetail);
                        orderDetailsEFsfromDB.Remove(existingOrderDetail);
                    }
                    else
                    {
                        // add new od
                        var nodEF = new OrderDetailsEF() { OrderUUID = oEF.OrderUUID, DishUUID = item.Key.DishUUID, DishAmount = item.Value };
                        ctx.OrderDetails.Add(nodEF);
                    }
                }
            }

            // remove existing ods (present in db, not in order)
            ctx.OrderDetails.RemoveRange(orderDetailsEFsfromDB);
            SaveAndClear();

            oEF.Dishes = null;
            ctx.Orders.Update(oEF);
            SaveAndClear();
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Update Order", ex);
        }
    }
}