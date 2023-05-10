using HorecaSwagger.BL.Model;
using HorecaSwagger.DLEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Mappers;

public static class DishMapper
{
    public static Dish MapToDomain(DishEF db)
    {
        try
        {
            return new Dish(db.DishUUID, db.Name, db.Description, db.PriceInEUR, db.AmountAvailable);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DishEF MapToDB(Dish dom, bool deleted)
    {
        try
        {
            return new DishEF()
            {
                DishUUID = dom.DishUUID,
                Name = dom.Name,
                Description = dom.Description,
                PriceInEUR= dom.PriceInEUR,
                AmountAvailable= dom.AmountAvailable,
                Deleted = deleted
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
