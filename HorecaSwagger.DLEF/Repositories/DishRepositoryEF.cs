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
        try
        {
            if (ctx.Dishes.Any(x => x.Name == dish.Name && x.Description == dish.Description))
            {
                //re add deleted dish
                var existingD = ctx.Dishes.Where(x => x.Name == dish.Name && x.Description == dish.Description).AsNoTracking().Single();
                existingD!.Deleted = false;
                ctx.Dishes.Update(existingD);
            }
            else
            {
                //add new dish
                ctx.Dishes.Add(DishMapper.MapToDB(dish, false));
            }
            SaveAndClear();
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Create Dish", ex);
        }
    }

    public void DeleteDish(int id)
    {
        try
        {
            ctx.Dishes.Update(DishMapper.MapToDB(Read(id), true));
            SaveAndClear();
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Delete Dish", ex);
        }
    }

    public Dish Read(int id)
    {
        try
        {
            return DishMapper.MapToDomain(ctx.Dishes.Where(x => x.DishUUID == id && x.Deleted == false).AsNoTracking().Single()!);
        }
        catch(Exception ex)
        {
            throw new RepositoryException($"Create Dish {id}", ex);
        }
    }

    public ICollection<Dish> ReadAll()
    {
        try
        {
            List<DishEF> dishesEFs = ctx.Dishes.Where(x => x.Deleted == false).AsNoTracking().ToList();
            return dishesEFs.Select(x => DishMapper.MapToDomain(x)).ToList();
        }
        catch(Exception ex)
        {
            throw new RepositoryException("Read Dishes", ex);
        }
    }

    public void UpdateDish(Dish dish)
    {
        try
        {
            ctx.Dishes.Update(DishMapper.MapToDB(dish, false));
            SaveAndClear();
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Update Dish", ex);
        }
    }
}
