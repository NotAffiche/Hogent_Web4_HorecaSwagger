using HorecaSwagger.BL.Model;
using HorecaSwagger.DLEF.Exceptions;
using HorecaSwagger.DLEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Mappers;

public static class OrderMapper
{
    public static Order MapToDomain(OrderEF db, List<OrderDetailsEF> odEFs)
    {
        try
        {
            Order dom = new Order(db.OrderUUID, db.CreateDate, db.PaymentDate, CustomerMapper.MapToDomain(db.Customer), null);
            foreach (OrderDetailsEF od in odEFs)
            {
                //dom.DishesWithAmount.Add();
            }
            return dom;
        }
        catch (Exception ex)
        {
            throw new MapperException("Order To Domain", ex);
        }
    }

    public static OrderEF MapToDB(Order dom, bool deleted)
    {
        try
        {
            return new OrderEF()
            {
                OrderUUID = dom.OrderUUID,
                CreateDate = dom.CreateDate,
                PaymentDate = dom.PaymentDate,
                Customer = CustomerMapper.MapToDB(dom.Customer, false),
                CustomerUUID = (int)dom.Customer.CustomerUUID,
                //Dishes = dom.DishesWithAmount.Select(x=> DishMapper.MapToDB(x, false)).ToList(),
                Deleted= deleted
            };
        }
        catch (Exception ex)
        {
            throw new MapperException("Order To DB", ex);
        }
    }
}
