using HorecaSwagger.API.DTO;
using HorecaSwagger.API.Exceptions;
using HorecaSwagger.BL.Model;

namespace HorecaSwagger.API.Mappers;

public static class DishMapper
{
    public static Dish MapToDomain(DishDTO dto)
    {
        try
        {
            return new Dish(dto.DishUUID, dto.Name, dto.Description, dto.PriceInEUR, dto.AmountAvailable);
        }
        catch (Exception ex)
        {
            throw new APIException("Dish To Domain", ex);
        }
    }

    public static DishDTO MapToDTO(Dish dom)
    {
        try
        {
            return new DishDTO(dom.DishUUID, dom.Name, dom.Description, dom.PriceInEUR, dom.AmountAvailable);
        }
        catch (Exception ex)
        {
            throw new APIException("Dish To DTO", ex);
        }
    }
}
