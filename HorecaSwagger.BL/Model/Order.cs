using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorecaSwagger.BL.Exceptions;

namespace HorecaSwagger.BL.Model;

public class Order
{
    private int _orderUUID;
    private DateTime _createDate;
    private DateTime? _paymentDate;
    private Customer _customer;

    public Order(DateTime createDate, DateTime? paymentDate, Customer customer, List<Dictionary<Dish, int>> dishesWithAmount)
    {
        CreateDate = createDate;
        PaymentDate = paymentDate;
        Customer = customer;
        DishesWithAmount = dishesWithAmount;
    }
    public Order(int orderUUID, DateTime createDate, DateTime? paymentDate, Customer customer, List<Dictionary<Dish, int>> dishesWithAmount) : this(createDate, paymentDate, customer, dishesWithAmount)
    {
        OrderUUID = orderUUID;
    }

    public int OrderUUID
    {
        get { return _orderUUID; }
        set
        {
            if (value < 0) throw new DomainException("Invalid OrderUUID", new ArgumentException());
            _orderUUID = value;
        }
    }
    public DateTime CreateDate
    {
        get { return _createDate; }
        set
        {
            if (value == default(DateTime)) throw new DomainException("Invalid CreateDate", new ArgumentException());
            _createDate = value;
        }
    }
    public DateTime? PaymentDate
    {
        get { return _paymentDate; }
        set
        {
            if (value!=null && DateTime.Compare((DateTime)value, CreateDate)<=0) throw new DomainException("Invalid PaymentDate", new ArgumentException());
            _paymentDate = value;
        }
    }
    public Customer Customer
    {
        get { return _customer; }
        set
        {
            if (value == null) throw new DomainException("Invalid Customer", new ArgumentNullException());
            _customer = value;
        }
    }
    public List<Dictionary<Dish, int>> DishesWithAmount { get; set; }
}
