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

public class DishRepositoryEF : IDishRepository
{
    private DataContext ctx;

    public DishRepositoryEF(string connStr)
    {
        ctx = new DataContext(connStr);
    }

    private void SaveAndClear()
    {
        ctx.SaveChanges();
        ctx.ChangeTracker.Clear();
    }

    public void CreateDish(Dish dish)
    {
        ctx.Dishes.Add(DishMapper.MapToDB(dish, false));
        SaveAndClear();
    }

    public void DeleteDish(Dish dish)
    {
        ctx.Dishes.Update(DishMapper.MapToDB(dish, true));
        SaveAndClear();
    }

    public Dish Read(int id)
    {
        return DishMapper.MapToDomain(ctx.Dishes.Where(x => x.DishUUID == id && x.Deleted == false).AsNoTracking().SingleOrDefault());
    }

    public ICollection<Dish> ReadAll()
    {
        List<DishEF> dishesEFs = ctx.Dishes.Where(x => x.Deleted == false).AsNoTracking().ToList();
        return dishesEFs.Select(x => DishMapper.MapToDomain(x)).ToList();
    }

    public void UpdateDish(Dish dish)
    {
        ctx.Dishes.Update(DishMapper.MapToDB(dish, false));
        SaveAndClear();
    }
}
