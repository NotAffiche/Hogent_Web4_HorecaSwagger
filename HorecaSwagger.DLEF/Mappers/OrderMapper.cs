﻿using HorecaSwagger.BL.Model;
using HorecaSwagger.DLEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Mappers;

public static class OrderMapper
{
    public static Order MapToDomain(OrderEF db)
    {
        try
        {
            Order dom = new Order(db.OrderUUID, db.CreateDate, db.PaymentDate, CustomerMapper.MapToDomain(db.Customer), db.Dishes.Select(x => DishMapper.MapToDomain(x)).ToList());
            return dom;
        }
        catch (Exception ex)
        {
            throw ex;
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
                CustomerUUID = dom.Customer.CustomerUUID,
                Dishes = dom.Dishes.Select(x=> DishMapper.MapToDB(x, false)).ToList(),
                Deleted= deleted
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
