using HorecaSwagger.API.DTO;
using HorecaSwagger.API.Exceptions;
using HorecaSwagger.BL.Model;
using HorecaSwagger.BL.Services;

namespace HorecaSwagger.API.Mappers;

public static class OrderMapper
{
    public static Order MapToDomain(OrderDTO dto, CustomerService customerService, DishService dishService)
    {
        try
        {
            List<Dictionary<Dish, int>> dishesWithAmount = new List<Dictionary<Dish, int>>();
            foreach (var item in dto.DishesWithAmount)
            {
                Dictionary<Dish, int> dWithAmount = new Dictionary<Dish, int>();
                foreach (var d in item)
                {
                    dWithAmount.Add(dishService.Read(d.Key), d.Value);
                }
                dishesWithAmount.Add(dWithAmount);
            }
            return new Order(dto.OrderUUID, dto.CreateDate, dto.PaymentDate, customerService.Read(dto.CustomerUUID), dishesWithAmount);
        }
        catch (Exception ex)
        {
            throw new APIException("Dish To Domain", ex);
        }
    }

    public static OrderDTO MapToDTO(Order dom)
    {
        try
        {
            List<Dictionary<int, int>> dishes = new List<Dictionary<int, int>>();
            foreach (var item in dom.DishesWithAmount)
            {
                Dictionary<int, int> dWithAmount= new Dictionary<int, int>();
                foreach (var d in item)
                {
                    dWithAmount.Add(d.Key.DishUUID, d.Value);
                }
                dishes.Add(dWithAmount);
            }
            return new OrderDTO(dom.OrderUUID, dom.CreateDate, dom.PaymentDate, dom.Customer.CustomerUUID, dishes);
        }
        catch (Exception ex)
        {
            throw new APIException("Dish To DTO", ex);
        }
    }
}
