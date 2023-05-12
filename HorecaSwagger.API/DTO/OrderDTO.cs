using HorecaSwagger.BL.Model;

namespace HorecaSwagger.API.DTO;

public class OrderDTO
{
    public OrderDTO()
    {

    }

    public OrderDTO(DateTime createDate, DateTime? paymentDate, int customerUUID, List<Dictionary<int, int>> dishesWithAmount)
    {
        CreateDate = createDate;
        PaymentDate = paymentDate;
        CustomerUUID = customerUUID;
        DishesWithAmount = dishesWithAmount;
    }

    public OrderDTO(int orderUUID, DateTime createDate, DateTime? paymentDate, int customerUUID, List<Dictionary<int, int>> dishesWithAmount) : this(createDate, paymentDate, customerUUID, dishesWithAmount)
    {
        OrderUUID = orderUUID;
    }

    public int OrderUUID { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public int CustomerUUID { get; set; }
    public List<Dictionary<int, int>> DishesWithAmount { get; set; }
}
