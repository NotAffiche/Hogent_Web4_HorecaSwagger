using HorecaSwagger.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Interfaces;

public interface IDishRepository
{
    void CreateDish(Dish dish);
    ICollection<Dish> ReadAll();
    Dish Read(int id);
    void UpdateDish(Dish dish);
    void DeleteDish(Dish dish);
}
