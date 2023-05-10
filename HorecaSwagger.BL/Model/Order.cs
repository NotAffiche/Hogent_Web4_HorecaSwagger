using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Model;

public class Order
{
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

    public int OrderUUID { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public Customer Customer { get; set; }
    public List<Dictionary<Dish, int>> DishesWithAmount { get; set; }
}
